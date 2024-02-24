﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Transaction;
using OnlineBanking.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ILogger = Serilog.ILogger;

namespace OnlineBanking.Application.Services
{
    public class UserService: IUserService
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
                    .Select(x => new UserProfileViewModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Firstname = x.Firstname,
                        Surname = x.Surname,
                        Street = x.Street,
                        City = x.City,
                        Role = x.Role,
                        ZipCode = x.ZipCode,
                        CreatedAt = x.CreatedAt,
                        Image = x.Avatar,
                        UserAccounts = x.Accounts.Select(a => new AccountViewModel()
                        {
                            Id = a.Id,
                            AccountName = a.AccountName,
                            AccountType = a.AccountType,
                            BalanceAmount = a.BalanceAmount,
                            CreatedAt = a.CreatedAt
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

        public async Task<Result<UserProfileViewModel>> EditUserInfo(UserProfileViewModel model, byte[] imageData)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefaultAsync();

                user.Avatar = imageData;

                await _userRepository.UpdateAsync(user);

                return new Result<UserProfileViewModel>()
                {
                    Data = model
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
