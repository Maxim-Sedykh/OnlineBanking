using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.AccountType;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Account> _accountReporisoty;
        private readonly IBaseRepository<AccountType> _accountTypeReporisoty;
        private readonly IBaseRepository<User> _userReporisoty;
        private readonly ILogger _logger;

        public AccountService(IBaseRepository<Account> accountReporisoty, ILogger logger, IBaseRepository<User> userReporisoty,
            IBaseRepository<AccountType> accountTypeReporisoty)
        {
            _accountReporisoty = accountReporisoty;
            _logger = logger;
            _userReporisoty = userReporisoty;
            _accountTypeReporisoty = accountTypeReporisoty;
        }

        public async Task<Result<Account>> AddMoneyToAccount(AccountMoneyViewModel viewModel)
        {
            try
            {
                var account = await _accountReporisoty.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
                if (account == null)
                {
                    return new Result<Account>
                    {
                        ErrorCode = (int)StatusCode.AccountAlreadyExist,
                        ErrorMessage = ErrorMessage.AccountAlreadyExist,
                    };
                }

                account.BalanceAmount += viewModel.BalanceAmount;

                await _accountReporisoty.UpdateAsync(account);

                return new Result<Account>
                {
                    Data = account,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<Account>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<Account>> CreateNewAccount(CreateAccountViewModel viewModel, string Username)
        {
            try
            {
                var account = await _accountReporisoty.GetAll().FirstOrDefaultAsync(x => x.AccountName == viewModel.AccountName);
                if (account != null)
                {
                    return new Result<Account>
                    {
                        ErrorCode = (int)StatusCode.AccountAlreadyExist,
                        ErrorMessage = ErrorMessage.AccountAlreadyExist,
                    };
                }

                var user = await _userReporisoty.GetAll().FirstOrDefaultAsync(x => x.Username == Username);
                if (user == null)
                {
                    return new Result<Account>
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                var accountType = await _accountTypeReporisoty.GetAll().FirstOrDefaultAsync(x => x.AccountTypeName == viewModel.SelectedAccountType);
                if (accountType == null)
                {
                    return new Result<Account>
                    {
                        ErrorCode = (int)StatusCode.AccountTypeNotFound,
                        ErrorMessage = ErrorMessage.AccountTypeNotFound,
                    };
                }


                account = new Account()
                {
                    AccountName = viewModel.AccountName,
                    UserId = user.Id,
                    AccountTypeId = accountType.Id,
                    BalanceAmount = 0.00m,
                    CreatedAt = DateTime.UtcNow,
                };

                await _accountReporisoty.CreateAsync(account);

                return new Result<Account>
                {
                    Data = account,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<Account>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<bool>> DeleteAccountById(AccountDeleteViewModel viewModel)
        {
            try
            {
                var account = await _accountReporisoty.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
                if (account == null)
                {
                    return new Result<bool>()
                    {
                        ErrorCode = (int)StatusCode.AccountNotFound,
                        ErrorMessage = ErrorMessage.AccountNotFound,
                    };
                }

                if (account.BalanceAmount != 0.00m)
                {
                    return new Result<bool>()
                    {
                        ErrorCode = (int)StatusCode.AccountBalanceNotEmpty,
                        ErrorMessage = ErrorMessage.AccountBalanceNotEmpty,
                    };
                }

                await _accountReporisoty.RemoveAsync(account);

                return new Result<bool>()
                {
                    SuccessMessage = SuccessMessage.DeleteAccountMessage,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<bool>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<AccountMoneyViewModel>> GetAccountById(int id)
        {
            try
            {
                var account = await _accountReporisoty.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (account == null)
                {
                    return new Result<AccountMoneyViewModel>()
                    {
                        ErrorCode = (int)StatusCode.AccountNotFound,
                        ErrorMessage = ErrorMessage.AccountNotFound,
                    };
                }

                return new Result<AccountMoneyViewModel>()
                {
                    Data = account.Adapt<AccountMoneyViewModel>(),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<AccountMoneyViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<CreateAccountViewModel>> GetAccountTypeNames()
        {
            try
            {
                var accountTypes = await _accountTypeReporisoty.GetAll()
                    .Select(u => u.AccountTypeName).ToListAsync();
                if (accountTypes == null)
                {
                    return new Result<CreateAccountViewModel>()
                    {
                        ErrorCode = (int)StatusCode.AccountTypesNotFound,
                        ErrorMessage = ErrorMessage.AccountTypesNotFound,
                    };
                }

                return new Result<CreateAccountViewModel>()
                {
                    Data = new CreateAccountViewModel
                    {
                        AccountTypes = accountTypes,
                    },
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<CreateAccountViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<CollectionResult<AccountTypeViewModel>> GetAccountTypes()
        {
            AccountTypeViewModel[] accountTypes;
            try
            {
                accountTypes = await _accountTypeReporisoty.GetAll()
                    .Select(x => new AccountTypeViewModel
                    {
                        Id = x.Id,
                        AccountTypeName = x.AccountTypeName,
                        Description = x.Description,
                        AnnualInterestRate  = x.AnnualInterestRate,
                        CreatedAt = x.CreatedAt,
                    })
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<AccountTypeViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError
                };
            }

            if (!accountTypes.Any())
            {
                _logger.Warning(ErrorMessage.AccountTypesNotFound, accountTypes.Length);
                return new CollectionResult<AccountTypeViewModel>()
                {
                    ErrorMessage = ErrorMessage.AccountTypesNotFound,
                    ErrorCode = (int)StatusCode.AccountTypesNotFound
                };
            }

            return new CollectionResult<AccountTypeViewModel>()
            {
                Data = accountTypes,
                Count = accountTypes.Length
            };
        }
    }
}
