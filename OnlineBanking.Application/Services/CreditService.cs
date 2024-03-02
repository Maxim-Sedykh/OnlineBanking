using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.CreditType;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class CreditService : ICreditService
    {
        private const decimal MIN_LIVING_WAGE = 12000;
        private const string DEFAULT_CREDIT_NAME = "Кредитный";

        private readonly IBaseRepository<Credit> _creditRepository;
        private readonly IBaseRepository<CreditType> _creditTypeRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<AccountType> _accountTypeRepository;
        private readonly ILogger _logger;

        public CreditService(IBaseRepository<Credit> creditRepository, IBaseRepository<CreditType> creditTypeRepository, ILogger logger,
            IBaseRepository<User> userRepository, IBaseRepository<Account> accountRepository, IBaseRepository<AccountType> accountTypeRepository)
        {
            _creditRepository = creditRepository;
            _creditTypeRepository = creditTypeRepository;
            _logger = logger;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _accountTypeRepository = accountTypeRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<CreditViewModel>> CreateCredit(CreateCreditViewModel viewModel, string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.UserProfile)
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new Result<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                if (!user.UserProfile.IsIncomeVerified)
                {
                    return new Result<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserIncomeNotVerified,
                        ErrorMessage = ErrorMessage.UserIncomeNotVerified,
                    };
                }

                var creditType = await _creditTypeRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedCreditTypeId);

                if (creditType == null)
                {
                    return new Result<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.CreditTypeNotFound,
                        ErrorMessage = ErrorMessage.CreditTypeNotFound,
                    };
                }

                if (viewModel.CreditTerm > DateTime.UtcNow.AddMonths(creditType.MaxCreaditTermInMonths) ||
                    viewModel.CreditTerm < DateTime.UtcNow.AddMonths(creditType.MinCreaditTermInMonths))
                {
                    return new Result<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.InvalidCreditTerm,
                        ErrorMessage = ErrorMessage.InvalidCreditTerm, 
                    };
                }

                decimal newCreditSumAmount = viewModel.MoneyLenderReceiveAmount + viewModel.MoneyLenderReceiveAmount * (decimal)(creditType.YearPercent / 100);
                decimal newCreditMounthPayment = Math.Round((newCreditSumAmount / GetMounthsDifference(DateTime.UtcNow, viewModel.CreditTerm)), 2, MidpointRounding.AwayFromZero);

                user.UserProfile.MonthlyCreditsPayment += newCreditMounthPayment;
                user.UserProfile.CreditsCount++;

                if (user.UserProfile.Income - (user.UserProfile.MonthlyCreditsPayment) < MIN_LIVING_WAGE)
                {
                    return new Result<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.CreditNotApproved,
                        ErrorMessage = ErrorMessage.CreditNotApproved,
                    };
                }

                var creditAccountType = await _accountTypeRepository.GetAll().FirstOrDefaultAsync(x => x.AccountTypeName == DEFAULT_CREDIT_NAME);

                Account account = new Account()
                {
                    AccountName = DEFAULT_CREDIT_NAME + user.UserProfile.CreditsCount,
                    UserId = user.Id,
                    BalanceAmount = viewModel.MoneyLenderReceiveAmount,
                    AccountTypeId = creditAccountType.Id,
                    IsCardLinked = false,
                    CreatedAt = DateTime.UtcNow,
                };

                Credit credit = new Credit()
                {
                    UserId = user.Id,
                    CreditSumAmount = newCreditSumAmount,
                    MoneyLenderReceiveAmount = viewModel.MoneyLenderReceiveAmount,
                    CreditTerm = DateTime.SpecifyKind(viewModel.CreditTerm, DateTimeKind.Utc),
                    CreditRemainerAmount = newCreditSumAmount,
                    MonthlyPayment = newCreditMounthPayment,
                    CreditTypeId = viewModel.SelectedCreditTypeId,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.UpdateAsync(user);
                await _accountRepository.CreateAsync(account);
                await _creditRepository.CreateAsync(credit);

                return new Result<CreditViewModel>()
                {
                    Data = credit.Adapt<CreditViewModel>(),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<CreditViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        /// <inheritdoc/>
        public async Task<Result<CreateCreditViewModel>> GetCreditTypesToSelect()
        {
            try
            {
                var creditTypes = await _creditTypeRepository.GetAll()
                    .Select(x => new SelectCreditTypeViewModel
                    {
                        Id = x.Id,
                        CreditTypeName = x.CreditTypeName,
                    })
                    .ToListAsync();

                if (creditTypes == null)
                {
                    return new Result<CreateCreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.CreditTypeNotFound,
                        ErrorMessage = ErrorMessage.CreditTypeNotFound
                    };
                }

                return new Result<CreateCreditViewModel>()
                {
                    Data = new CreateCreditViewModel()
                    {
                        CreditTypes = creditTypes
                    },
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<CreateCreditViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError,
                };
            }
        }

        /// <inheritdoc/>
        public Task<CollectionResult<CreditTypeViewModel>> GetCreditTypes()
        {
            try
            {
                var creditTypes = _creditTypeRepository.GetAll()
                    .Select(x => new CreditTypeViewModel
                    {
                        Id = x.Id,
                        CreditTypeName = x.CreditTypeName,
                        Description = x.Description,
                        MinCreaditTermInMonths = x.MinCreaditTermInMonths,
                        MaxCreaditTermInMonths = x.MaxCreaditTermInMonths,
                        InterestRate = x.YearPercent,
                    })
                    .AsEnumerable();

                if (creditTypes == null)
                {
                    return Task.FromResult(new CollectionResult<CreditTypeViewModel>()
                    {
                        ErrorCode = (int)StatusCode.CreditTypesNotFound,
                        ErrorMessage = ErrorMessage.CreditTypesNotFound
                    });
                }

                return Task.FromResult(new CollectionResult<CreditTypeViewModel>()
                {
                    Data = creditTypes,
                    Count = creditTypes.Count(),
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new CollectionResult<CreditTypeViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError,
                });
            }
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<CreditViewModel>> GetUserCredits(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new CollectionResult<CreditViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                var userCredits = await _creditRepository.GetAll()
                    .Include(x => x.CreditType)
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new CreditViewModel
                    {
                        Id = x.Id,
                        CreditTypeName = x.CreditType.CreditTypeName,
                        CreditSumAmount = x.CreditSumAmount,
                        MoneyLenderReceiveAmount = x.MoneyLenderReceiveAmount,
                        CreditTerm = x.CreditTerm,
                        Percent = x.CreditType.YearPercent,
                        CreatedAt = x.CreatedAt,
                        CreditRemainerAmount = x.CreditRemainerAmount,
                        MonthlyPayment = x.MonthlyPayment
                    })
                    .ToArrayAsync();

                return new CollectionResult<CreditViewModel>()
                {
                    Data = userCredits,
                    Count = userCredits.Length,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<CreditViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        /// <inheritdoc/>
        public async Task<Result<User>> SetUserIncome(SetIncomeViewModel viewModel, string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.UserProfile)
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new Result<User>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                user.UserProfile.Income = viewModel.UserIncome;
                user.UserProfile.IsIncomeVerified = true; //Изначально подразумевается, что пользователь должен подтвердить свой доход различными справками,
                                    //в качестве теста, доход будет сразу утверждён
                await _userRepository.UpdateAsync(user);

                return new Result<User>()
                {
                    Data = user,
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<User>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        /// <inheritdoc/>
        public async Task<Result<SetIncomeViewModel>> GetIncomeViewModel(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.UserProfile)
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new Result<SetIncomeViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                return new Result<SetIncomeViewModel>()
                {
                    Data = new SetIncomeViewModel()
                    {
                        IsIncomeVerified = user.UserProfile.IsIncomeVerified,
                        UserIncome = user.UserProfile.IsIncomeVerified ? user.UserProfile.Income : default,
                    },
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<SetIncomeViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        private int GetMounthsDifference(DateTime first, DateTime second)
        { 
            return ((second.Year - first.Year) * 12) + second.Month - first.Month;
        }
    }
}
