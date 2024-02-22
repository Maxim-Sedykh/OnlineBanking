using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Transaction;
using OnlineBanking.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ILogger = Serilog.ILogger;

namespace OnlineBanking.Application.Services
{
    public class UserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<Transaction> _transactionRepository;
        private readonly ILogger _logger;

        public UserService(IBaseRepository<User> userRepository, ILogger logger, IBaseRepository<Account> accountRepository,
            IBaseRepository<Transaction> transactionRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<UserProfileViewModel>> GetUserProfile(string userName)
        {
            try
            {
                var userProfileViewModel = await _userRepository.GetAll()
                    .Where(x => x.Username == userName)
                    .Include(x => x.Accounts)
                    .Include(x => x.RecicipientTransactions)
                        .ThenInclude(t => t.Recipient)
                    .Include(x => x.SenderTransactions)
                        .ThenInclude(t => t.Sender)
                    .Include(x => x.SenderTransactions)
                        .ThenInclude(t => t.PaymentMethod)
                    .Select(x => new UserProfileViewModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Firstname = x.Firstname,
                        Surname = x.Surname,
                        Street = x.Street,
                        City = x.City,
                        ZipCode = x.ZipCode,
                        CreatedAt = x.CreatedAt,
                        UserAccounts = x.Accounts.Select(a => new AccountViewModel()
                        {
                            AccountName = a.AccountName,
                            AccountType = a.AccountType,
                            BalanceAmount = a.BalanceAmount,
                            CreatedAt = a.CreatedAt
                        }).ToList(),
                        UserTransactions = x.SenderTransactions.Select(t => new TransactionViewModel()
                        {
                            SenderName = t.Sender.Username,
                            RecipientName = t.Recipient.Username,
                            MoneyAmount = t.MoneyAmount,
                            PaymentMethodName = t.PaymentMethod.Name,
                            TransactionDate = t.TransactionDate
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return new Result<UserProfileViewModel>()
                {
                    Data = userProfileViewModel
                };


            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<UserProfileViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError
                };
            }
        }
    }
}
