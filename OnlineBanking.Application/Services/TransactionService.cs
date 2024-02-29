using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Resources.Success;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Auth;
using OnlineBanking.Domain.ViewModel.PaymentMethod;
using OnlineBanking.Domain.ViewModel.Transaction;
using Serilog;

namespace OnlineBanking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IBaseRepository<Transaction> _transactionRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<PaymentMethod> _paymentMethodRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<Card> _cardRepository;
        private readonly ILogger _logger;

        public TransactionService(IBaseRepository<Transaction> transactionRepository, IBaseRepository<User> userRepository, ILogger logger,
            IBaseRepository<Account> accountRepository, IBaseRepository<PaymentMethod> paymentMethodRepository, IBaseRepository<Card> cardRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _logger = logger;
            _accountRepository = accountRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _cardRepository = cardRepository;
        }

        public async Task<Result<CreateTransactionViewModel>> GetDataToMakeTransaction(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (user == null)
                {
                    return new Result<CreateTransactionViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                var paymentMethodsNames = await _paymentMethodRepository.GetAll()
                    .Select(x => new SelectPaymentMethodViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToListAsync();

                if (paymentMethodsNames.Count == 0)
                {
                    return new Result<CreateTransactionViewModel>()
                    {
                        ErrorCode = (int)StatusCode.PaymentMethodsNotFound,
                        ErrorMessage = ErrorMessage.PaymentMethodsNotfound,
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

              
                return new Result<CreateTransactionViewModel>()
                {
                    Data = new CreateTransactionViewModel()
                    {
                        PaymentMethodNames = paymentMethodsNames,
                        UserAccounts = userAccounts,
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<CreateTransactionViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<TransactionPageViewModel>> GetUserTransactions(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

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
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                if (user == null)
                {
                    return new Result<TransactionPageViewModel>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
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
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<TransactionPageViewModel>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }

        public async Task<Result<Transaction>> MakeTransaction(CreateTransactionViewModel viewModel, string userName)
        {
            try
            {
                var account = await _accountRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == viewModel.SelectedUserAccountId);

                if (account == null)
                {
                    return new Result<Transaction>()
                    {
                        ErrorCode = (int)StatusCode.AccountNotFound,
                        ErrorMessage = ErrorMessage.AccountNotFound,
                    };
                }

                if (account.BalanceAmount < viewModel.MoneyAmount)
                {
                    return new Result<Transaction>()
                    {
                        ErrorCode = (int)StatusCode.NotEnoughFunds,
                        ErrorMessage = ErrorMessage.NotEnoughFunds,
                    };
                }

                var sender = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                if (sender == null)
                {
                    return new Result<Transaction>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                var recipientCard = await _cardRepository.GetAll()
                    .Include(x => x.Account)
                    .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => x.CardNumber == viewModel.RecipientCardNumber);

                if (recipientCard == null)
                {
                    return new Result<Transaction>()
                    {
                        ErrorCode = (int)StatusCode.CardNotFound,
                        ErrorMessage = ErrorMessage.CardNotFound,
                    };
                }

                account.BalanceAmount -= viewModel.MoneyAmount;

                recipientCard.Account.BalanceAmount += viewModel.MoneyAmount;

                Transaction transaction = new Transaction()
                {
                    SenderId = sender.Id,
                    RecipientId = recipientCard.Account.UserId,
                    MoneyAmount = viewModel.MoneyAmount,
                    PaymentMethodId = viewModel.SelectedPaymentMethodId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _accountRepository.UpdateAsync(account);
                await _cardRepository.UpdateAsync(recipientCard);

                await _transactionRepository.CreateAsync(transaction);

                return new Result<Transaction>()
                {
                    Data = transaction,
                    SuccessMessage = SuccessMessage.CompleteTransaction,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<Transaction>()
                {
                    ErrorCode = (int)StatusCode.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError,
                };
            }
        }
    }
}
