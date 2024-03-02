﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
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
    /// <inheritdoc/>
    public class UserProfileService: IUserProfileService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger _logger;

        public UserProfileService(IBaseRepository<User> userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Result<UserProfileViewModel>> GetUserProfile(string userName)
        {
            try
            {
                var userProfileViewModel = await _userRepository.GetAll()
                    .Where(x => x.Username == userName)
                    .Include(x => x.UserProfile)
                    .Include(x => x.Accounts)
                    .ThenInclude(x => x.AccountType)
                    .Select(x => new UserProfileViewModel()
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Firstname = x.UserProfile.Firstname,
                        Surname = x.UserProfile.Surname,
                        Street = x.UserProfile.Street,
                        City = x.UserProfile.City,
                        Role = x.Role,
                        ZipCode = x.UserProfile.ZipCode,
                        CreatedAt = x.CreatedAt,
                        Image = x.UserProfile.Avatar,
                        UserAccounts = x.Accounts.Select(a => new AccountViewModel()
                        {
                            Id = a.Id,
                            AccountName = a.AccountName,
                            AccountTypeName = a.AccountType.AccountTypeName,
                            IsCardLinked = a.IsCardLinked,
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

        /// <inheritdoc/>
        public async Task<Result<UserProfileViewModel>> EditUserInfo(UserProfileViewModel viewModel, byte[] imageData)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.UserProfile)
                    .Where(x => x.Id == viewModel.Id)
                    .FirstOrDefaultAsync();

                user.UserProfile.Avatar = imageData;

                await _userRepository.UpdateAsync(user);

                return new Result<UserProfileViewModel>()
                {
                    Data = viewModel
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