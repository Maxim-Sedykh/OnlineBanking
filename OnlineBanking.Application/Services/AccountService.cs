using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
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
        private readonly IBaseRepository<User> _userReporisoty;
        private readonly ILogger _logger;

        public AccountService(IBaseRepository<Account> accountReporisoty, ILogger logger, IBaseRepository<User> userReporisoty)
        {
            _accountReporisoty = accountReporisoty;
            _logger = logger;
            _userReporisoty = userReporisoty;
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


                account = new Account()
                {
                    AccountName = viewModel.AccountName,
                    UserId = user.Id,
                    AccountType = viewModel.AccountType,
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
    }
}
