using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Application.Validators;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
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
using System.Security.Principal;
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
        private readonly IUserValidator _userValidator;

        public UserProfileService(IBaseRepository<User> userRepository, ILogger logger, IUserValidator userValidator)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userValidator = userValidator;
        }

        /// <inheritdoc/>
        public async Task<Result<UserProfileViewModel>> GetUserProfile(string userName)
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

        /// <inheritdoc/>
        public async Task<Result> EditUserInfo(UserProfileViewModel viewModel)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
            }

            var user = await _userRepository.GetAll()
                .Include(x => x.UserProfile)
                .Where(x => x.Id == viewModel.Id)
            .FirstOrDefaultAsync();

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess)
            {
                return nullValidationResult;
            }

            user.UserProfile.Avatar = imageData;

            await _userRepository.UpdateAsync(user);

            return new Result();
        }
    }
}
