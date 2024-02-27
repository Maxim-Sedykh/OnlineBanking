using Microsoft.EntityFrameworkCore;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Auth;
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
        private readonly ILogger _logger;

        public TransactionService(IBaseRepository<Transaction> transactionRepository, IBaseRepository<User> userRepository, ILogger logger,
            IBaseRepository<Account> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _logger = logger;
            _accountRepository = accountRepository;
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
                    .Select(x => x.Name)
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

        public async Task<Result<TransactionPageViewModel>> GetUserTransactions(LoginUserViewModel viewModel, string userName)
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
                        PaymentMethodName = x.PaymentMethod.ToString(),
                        CreatedAt = x.CreatedAt,
                    })
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
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Username == userName);

                var userTransactions = await _transactionRepository.GetAll()
                    .Where(x => x.SenderId == user.Id || x.RecipientId == user.Id)
                    .Include(x => x.Recipient)
                    .Include(x => x.Sender)
                    .Select(x => new TransactionViewModel
                    {
                        Id = x.Id,
                        SenderName = x.Sender.Username,
                        RecipientName = x.Recipient.Username,
                        MoneyAmount = x.MoneyAmount,
                        PaymentMethodName = x.PaymentMethod.ToString(),
                        CreatedAt = x.CreatedAt,
                    })
                    .ToListAsync();

                if (user == null)
                {
                    return new Result<Transaction>()
                    {
                        ErrorCode = (int)StatusCode.UserNotFound,
                        ErrorMessage = ErrorMessage.UserNotFound,
                    };
                }

                return new Result<Transaction>()
                {
                    Data = new Transaction()
                    {
                        Transactions = userTransactions,
                    }
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
