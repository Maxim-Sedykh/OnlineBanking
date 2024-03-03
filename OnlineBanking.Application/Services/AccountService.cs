using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.AccountType;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Account> _accountReporisoty;
        private readonly IBaseRepository<AccountType> _accountTypeReporisoty;
        private readonly IBaseRepository<User> _userReporisoty;
        private readonly IAccountValidator _accountValidator;
        private readonly IUserValidator _userValidator;
        private readonly IAccountTypeValidator _accountTypeValidator;

        public AccountService(IBaseRepository<Account> accountReporisoty, IBaseRepository<User> userReporisoty,
            IBaseRepository<AccountType> accountTypeReporisoty, IUserValidator userValidator, IAccountTypeValidator accountTypeValidator, IAccountValidator accountValidator)
        {
            _accountReporisoty = accountReporisoty;
            _userReporisoty = userReporisoty;
            _accountTypeReporisoty = accountTypeReporisoty;
            _userValidator = userValidator;
            _accountTypeValidator = accountTypeValidator;
            _accountValidator = accountValidator;
        }

        /// <inheritdoc/>
        public async Task<Result> AddMoneyToAccount(AccountMoneyViewModel viewModel)
        {
            var account = await _accountReporisoty.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            var nullValidationResult = _accountValidator.ValidateEntityOnNull(account);
            if (!nullValidationResult.IsSuccess) 
            {
                return nullValidationResult;
            }

            account.BalanceAmount += viewModel.BalanceAmount;

            await _accountReporisoty.UpdateAsync(account);

            return new Result();
        }

        /// <inheritdoc/>
        public async Task<Result> CreateNewAccount(CreateAccountViewModel viewModel, string userName)
        {
            var user = await _userReporisoty.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess) return nullValidationResult;

            Account account = new Account()
            {
                AccountName = viewModel.AccountName,
                UserId = user.Id,
                AccountTypeId = viewModel.SelectedAccountTypeId,
                BalanceAmount = 0.00m,
                CreatedAt = DateTime.UtcNow,
            };

            await _accountReporisoty.CreateAsync(account);

            return new Result();
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteAccountById(AccountDeleteViewModel viewModel)
        {
            var account = await _accountReporisoty.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            var nullValidationResult = _accountValidator.ValidateEntityOnNull(account);
            if (!nullValidationResult.IsSuccess) return nullValidationResult;

            var balanceValidationResult = _accountValidator.ValidateBalance(account.BalanceAmount);
            if (!nullValidationResult.IsSuccess) return nullValidationResult;

            await _accountReporisoty.RemoveAsync(account);

            return new Result()
            {
                SuccessMessage = SuccessMessage.DeleteAccountMessage,
            };
        }

        /// <inheritdoc/>
        public async Task<Result<AccountMoneyViewModel>> GetAccountById(int id)
        {
            var account = await _accountReporisoty.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            var nullValidationResult = _accountValidator.ValidateEntityOnNull(account);
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<AccountMoneyViewModel>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
                };
            }

            return new Result<AccountMoneyViewModel>()
            {
                Data = account.Adapt<AccountMoneyViewModel>(),
            };
        }

        /// <inheritdoc/>
        public async Task<Result<CreateAccountViewModel>> GetAccountTypeNames()
        {
            var accountTypes = await _accountTypeReporisoty.GetAll()
                .Select(x => new SelectAccountTypeViewModel
                {
                    Id = x.Id,
                    AccountTypeName = x.AccountTypeName,
                }).ToListAsync();

            var nullValidationResult = _accountTypeValidator.ValidateAccountTypesOnNull(accountTypes.AsEnumerable());
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<CreateAccountViewModel>
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
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

        /// <inheritdoc/>
        public async Task<CollectionResult<AccountTypeViewModel>> GetAccountTypes()
        {
            AccountTypeViewModel[] accountTypes;

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


            if (accountTypes == null)
            {
                return new CollectionResult<AccountTypeViewModel>
                {
                    ErrorMessage = ErrorMessage.AccountTypesNotFound,
                    ErrorCode = (int)StatusCode.AccountTypesNotFound,
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
