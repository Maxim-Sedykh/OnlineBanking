using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Validators;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.CreditType;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class CreditService : ICreditService
    {
        private const string DEFAULT_CREDIT_NAME = "Кредитный";

        private readonly IBaseRepository<Credit> _creditRepository;
        private readonly IBaseRepository<CreditType> _creditTypeRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<AccountType> _accountTypeRepository;
        private readonly IUserValidator _userValidator;
        private readonly ICreditTypeValidator _creditTypeValidator;
        private readonly ICreditValidator _creditValidator;

        public CreditService(IBaseRepository<Credit> creditRepository, IBaseRepository<CreditType> creditTypeRepository,
            IBaseRepository<User> userRepository, IBaseRepository<Account> accountRepository, IBaseRepository<AccountType> accountTypeRepository,
            IUserValidator userValidator, ICreditTypeValidator creditTypeValidator, ICreditValidator creditValidator)
        {
            _creditRepository = creditRepository;
            _creditTypeRepository = creditTypeRepository;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _accountTypeRepository = accountTypeRepository;
            _userValidator = userValidator;
            _creditTypeValidator = creditTypeValidator;
            _creditValidator = creditValidator;
        }

        /// <inheritdoc/>
        public async Task<Result> CreateCredit(CreateCreditViewModel viewModel, string userName)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.Username == userName);

            var userNullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!userNullValidationResult.IsSuccess)
            {
                return userNullValidationResult;
            }

            if (!user.UserProfile.IsIncomeVerified)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.UserIncomeNotVerified,
                    ErrorMessage = ErrorMessage.UserIncomeNotVerified,
                };
            }
            
            var creditType = await _creditTypeRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedCreditTypeId);

            var creditTypeNullValidationResult = _creditTypeValidator.ValidateEntityOnNull(creditType);
            if (!creditTypeNullValidationResult.IsSuccess)
            {
                return creditTypeNullValidationResult;
            }

            var creditTermValidationResult = _creditValidator.ValidateCreditByTerm(viewModel.CreditTerm, creditType);
            if (!creditTypeNullValidationResult.IsSuccess) 
            {
                return creditTypeNullValidationResult;
            }
            
            decimal newCreditSumAmount = GetCreditSumAmount(viewModel.MoneyLenderReceiveAmount, creditType.YearPercent);
            decimal newCreditMounthPayment = GetCreditMounthPayment(newCreditSumAmount, viewModel.CreditTerm);

            user.UserProfile.MonthlyCreditsPayment += newCreditMounthPayment;
            user.UserProfile.CreditsCount++;

            var creditVerifyValidationResult = _creditValidator.ValidateCreditVerify(user.UserProfile.Income, user.UserProfile.MonthlyCreditsPayment);
            if (!creditTypeNullValidationResult.IsSuccess)
            {
                return creditTypeNullValidationResult;
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

            return new Result();
        }

        private decimal GetCreditSumAmount(decimal moneyLenderReceiveAmount, float yearPercent)
        {
            return moneyLenderReceiveAmount + moneyLenderReceiveAmount * (decimal)(yearPercent / 100);
        }

        private decimal GetCreditMounthPayment(decimal newCreditSumAmount, DateTime creditTerm)
        {
            return Math.Round((newCreditSumAmount / GetMounthsDifference(DateTime.UtcNow, creditTerm)), 2, MidpointRounding.AwayFromZero);
        }

        /// <inheritdoc/>
        public async Task<Result<CreateCreditViewModel>> GetCreditTypesToSelect()
        {
            var creditTypes = await _creditTypeRepository.GetAll()
                .Select(x => new SelectCreditTypeViewModel
                {
                    Id = x.Id,
                    CreditTypeName = x.CreditTypeName,
                })
                .ToListAsync();

            var nullValidationResult = _creditTypeValidator.ValidateCreditTypesOnNull(creditTypes.AsEnumerable());
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<CreateCreditViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
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

        /// <inheritdoc/>
        public async Task<CollectionResult<CreditTypeViewModel>> GetCreditTypes()
        {
            var creditTypes = await _creditTypeRepository.GetAll()
                .Select(x => new CreditTypeViewModel
                {
                    Id = x.Id,
                    CreditTypeName = x.CreditTypeName,
                    Description = x.Description,
                    MinCreaditTermInMonths = x.MinCreaditTermInMonths,
                    MaxCreaditTermInMonths = x.MaxCreaditTermInMonths,
                    InterestRate = x.YearPercent,
                })
                .ToListAsync();

            var nullValidationResult = _creditTypeValidator.ValidateCreditTypesOnNull(creditTypes.AsEnumerable());
            if (!nullValidationResult.IsSuccess)
            {
                return new CollectionResult<CreditTypeViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
                };
            }

            return new CollectionResult<CreditTypeViewModel>()
            {
                Data = creditTypes,
                Count = creditTypes.Count,
            };
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<CreditViewModel>> GetUserCredits(string userName)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Username == userName);

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess)
            {
                return new CollectionResult<CreditViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
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

        /// <inheritdoc/>
        public async Task<Result> SetUserIncome(SetIncomeViewModel viewModel, string userName)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.Username == userName);

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess) return nullValidationResult;

            user.UserProfile.Income = viewModel.UserIncome;
            user.UserProfile.IsIncomeVerified = true; //Изначально подразумевается, что пользователь должен подтвердить свой доход различными справками,
                                //в качестве теста, доход будет сразу утверждён
            await _userRepository.UpdateAsync(user);

            return new Result();
        }

        /// <inheritdoc/>
        public async Task<Result<SetIncomeViewModel>> GetIncomeViewModel(string userName)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.Username == userName);

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<SetIncomeViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
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

        private int GetMounthsDifference(DateTime first, DateTime second)
        { 
            return ((second.Year - first.Year) * 12) + second.Month - first.Month;
        }
    }
}
