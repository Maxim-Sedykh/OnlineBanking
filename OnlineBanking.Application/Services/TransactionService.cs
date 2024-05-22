using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Application.Validators;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Extensions;
using OnlineBanking.Domain.Interfaces.Database;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Auth;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.PaymentMethod;
using OnlineBanking.Domain.ViewModel.Transaction;
using Serilog;
using System.Security.Principal;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class TransactionService : ITransactionService
    {
        private readonly IBaseRepository<Transaction> _transactionRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<PaymentMethod> _paymentMethodRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<Card> _cardRepository;
        private readonly IBaseRepository<Credit> _creditRepository;
        private readonly IUserValidator _userValidator;
        private readonly IAccountValidator _accountValidator;
        private readonly ITransactionValidator _transactionValidator;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;


        public TransactionService(IBaseRepository<Transaction> transactionRepository, IBaseRepository<User> userRepository,
            IBaseRepository<Account> accountRepository, IBaseRepository<PaymentMethod> paymentMethodRepository, IBaseRepository<Card> cardRepository,
            IBaseRepository<Credit> creditRepository, IUserValidator userValidator, IAccountValidator accountValidator, ITransactionValidator transactionValidator,
            ILogger logger, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _cardRepository = cardRepository;
            _creditRepository = creditRepository;
            _userValidator = userValidator;
            _accountValidator = accountValidator;
            _transactionValidator = transactionValidator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<Result<CreateTransactionViewModel>> GetDataToMakeTransaction(string userName)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Username == userName);

            var userValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!userValidationResult.IsSuccess)
            {
                return new Result<CreateTransactionViewModel>()
                {
                    ErrorMessage = userValidationResult.ErrorMessage,
                    ErrorCode = userValidationResult.ErrorCode,
                };
            }

            var paymentMethodsNames = await _paymentMethodRepository.GetAll()
                .Select(x => new SelectPaymentMethodViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            var paymentMethodValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!paymentMethodValidationResult.IsSuccess)
            {
                return new Result<CreateTransactionViewModel>()
                {
                    ErrorMessage = paymentMethodValidationResult.ErrorMessage,
                    ErrorCode = paymentMethodValidationResult.ErrorCode,
                };
            }

            var userAccounts = await _accountRepository.GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => new AccountMoneyViewModel
                {
                    Id = x.Id,
                    AccountName = x.AccountName,
                    BalanceAmount = x.BalanceAmount,
                })
                .ToListAsync();

            var userCreditsToSelect = await _creditRepository.GetAll()
                .Include(x => x.CreditType)
                .Where(x => x.UserId == user.Id)
                .Select(x => new SelectCreditViewModel
                {
                    Id = x.Id,
                    CreditTypeName = x.CreditType.CreditTypeName,
                    CreditRemainerAmount = x.CreditRemainerAmount,
                    MonthlyPayment = x.MonthlyPayment,
                    CreditTerm = x.CreditTerm
                })
                .ToListAsync();


            return new Result<CreateTransactionViewModel>()
            {
                Data = new CreateTransactionViewModel()
                {
                    PaymentMethodNames = paymentMethodsNames,
                    UserAccounts = userAccounts,
                    UserCredits = userCreditsToSelect,
                }
            };
        }

        /// <inheritdoc/>
        public async Task<Result<TransactionPageViewModel>> GetUserTransactions(string userName)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Username == userName);

            var userNullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!userNullValidationResult.IsSuccess)
            {
                return new Result<TransactionPageViewModel>()
                {
                    ErrorMessage = userNullValidationResult.ErrorMessage,
                    ErrorCode = userNullValidationResult.ErrorCode,
                };
            }

            var userTransactions = await _transactionRepository.GetAll()
                .Where(x => x.SenderId == user.Id || x.RecipientId == user.Id )
                .Include(x => x.Recipient)
                .Include(x => x.Sender)
                .Select(x => new TransactionViewModel
                {
                    Id = x.Id,
                    SenderName = x.Sender.Username,
                    RecipientName = x.Recipient.Username,
                    MoneyAmount = x.MoneyAmount,
                    PaymentMethodName = x.PaymentMethod.Name,
                    CreatedAt = x.CreatedAt,
                    TransactionType = x.TransactionType.GetDisplayName()
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var userValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!userValidationResult.IsSuccess)
            {
                return new Result<TransactionPageViewModel>()
                {
                    ErrorMessage = userValidationResult.ErrorMessage,
                    ErrorCode = userValidationResult.ErrorCode,
                };
            }

            return new Result<TransactionPageViewModel>()
            {
                Data = new TransactionPageViewModel()
                {
                    Transactions = userTransactions,
                }
            };
        }

        /// <inheritdoc/>
        public async Task<Result> MakeCreditTransaction(CreateTransactionViewModel viewModel, string userName)
        {
            var user = await _userRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Username == userName);

            var userNullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!userNullValidationResult.IsSuccess)
            {
                return userNullValidationResult;
            }

            var userAccount = await _accountRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedUserAccountId);

            var accountNullValidationResult = _accountValidator.ValidateEntityOnNull(userAccount);
            if (!accountNullValidationResult.IsSuccess)
            {
                return accountNullValidationResult;
            }

            var credit = await _creditRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedUserCreditId);

            var transactionValidation = _transactionValidator.ValidateAvailabilityAccountBalance(userAccount.BalanceAmount, viewModel.MoneyAmount);
            if (!transactionValidation.IsSuccess)
            {
                return transactionValidation;
            }

            ICreditValidator creditValidator = new CreditValidator();

            var creditNullValidationResult = creditValidator.ValidateEntityOnNull(credit);
            if (!creditNullValidationResult.IsSuccess)
            {
                return creditNullValidationResult;
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    userAccount.BalanceAmount -= viewModel.MoneyAmount;

                    credit.CreditRemainerAmount -= viewModel.MoneyAmount;

                    if (credit.CreditRemainerAmount <= 0)
                    {
                        await _creditRepository.RemoveAsync(credit);

                        await transaction.CommitAsync();

                        return new Result()
                        {
                            SuccessMessage = SuccessMessage.CloseCredit,
                        };
                    }

                    Transaction newTransaction = new Transaction()
                    {
                        SenderId = user.Id,
                        RecipientId = user.Id,
                        MoneyAmount = viewModel.MoneyAmount,
                        PaymentMethodId = viewModel.SelectedPaymentMethodId,
                        TransactionType = TransactionType.Credit,
                        CreatedAt = DateTime.UtcNow,
                    };

                    await _accountRepository.UpdateAsync(userAccount);
                    await _creditRepository.UpdateAsync(credit);

                    await _transactionRepository.CreateAsync(newTransaction);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    await transaction.RollbackAsync();
                }
            }

            return new Result()
            {
                SuccessMessage = SuccessMessage.CompleteTransaction,
            };
        }

        /// <inheritdoc/>
        public async Task<Result> MakeTransaction(CreateTransactionViewModel viewModel, string userName)
        {
            var account = await _accountRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedUserAccountId);

            var accountValidation = _accountValidator.ValidateEntityOnNull(account);
            if (!accountValidation.IsSuccess)
            {
                return accountValidation;
            }

            var transactionValidation = _transactionValidator.ValidateAvailabilityAccountBalance(account.BalanceAmount, viewModel.MoneyAmount);
            if (!transactionValidation.IsSuccess)
            {
                return transactionValidation;
            }

            var sender = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Username == userName);

            var userNullValidationResult = _userValidator.ValidateEntityOnNull(sender);
            if (!userNullValidationResult.IsSuccess)
            {
                return userNullValidationResult;
            }

            var recipientCard = await _cardRepository.GetAll()
                .Include(x => x.Account)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.CardNumber == viewModel.RecipientCardNumber);

            ICardValidator cardValidator = new CardValidator();

            var cardNullValidationResult = cardValidator.ValidateEntityOnNull(recipientCard);
            if (!cardNullValidationResult.IsSuccess)
            {
                return cardNullValidationResult;
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    account.BalanceAmount -= viewModel.MoneyAmount;

                    recipientCard.Account.BalanceAmount += viewModel.MoneyAmount;

                    Transaction newTransaction = new Transaction()
                    {
                        SenderId = sender.Id,
                        RecipientId = recipientCard.Account.UserId,
                        MoneyAmount = viewModel.MoneyAmount,
                        PaymentMethodId = viewModel.SelectedPaymentMethodId,
                        TransactionType = TransactionType.Common,
                        CreatedAt = DateTime.UtcNow,
                    };

                    await _accountRepository.UpdateAsync(account);
                    await _cardRepository.UpdateAsync(recipientCard);

                    await _transactionRepository.CreateAsync(newTransaction);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    await transaction.RollbackAsync();
                }
            }

            return new Result()
            {
                SuccessMessage = SuccessMessage.CompleteTransaction,
            };
        }
    }
}
