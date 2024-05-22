using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.Application.Resources.Error;
using OnlineBanking.DAL.Repositories;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Helpers;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Auth;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace OnlineBanking.Application.Services
{
    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<UserProfile> _userProfileRepository;
        private readonly IUserValidator _userValidator;

        public AuthService(IBaseRepository<User> userRepository, IBaseRepository<UserProfile> userProfileRepository, IUserValidator userValidator)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userValidator = userValidator;
        }

        /// <inheritdoc/>
        public async Task<Result<ClaimsIdentity>> Login(LoginUserViewModel viewModel)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == viewModel.Email);

            var nullValidationResult = _userValidator.ValidateEntityOnNull(user);
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<ClaimsIdentity>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
                };
            }

            if (!IsVerifyPassword(user.Password, viewModel.Password))
            {
                return new Result<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)StatusCode.PasswordIsWrong,
                };
            }

            var result = Authenticate(user);

            return new Result<ClaimsIdentity>()
            {
                Data = result,
            };
        }

        /// <inheritdoc/>
        public async Task<Result<ClaimsIdentity>> Register(RegisterUserViewModel model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                return new Result<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)StatusCode.PasswordNotEqualsPasswordConfirm,
                };
            }

            // Гипотетическая проверка, есть ли человек с такими паспортными данными

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.Username || x.PassportCode == model.PassportCode);
            var nullValidationResult = _userValidator.RegisterUserValidate(user);
            if (!nullValidationResult.IsSuccess)
            {
                return new Result<ClaimsIdentity>()
                {
                    ErrorMessage = nullValidationResult.ErrorMessage,
                    ErrorCode = nullValidationResult.ErrorCode,
                };
            }

            user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPasswordHelper.HashPassword(model.Password),
                Role = Role.User,
                CreatedAt = DateTime.UtcNow,
            };

            UserProfile userProfile = new UserProfile()
            {
                Firstname = model.Firstname,
                Surname = model.Surname,
                IsOnlineBankingUser = true,
                City = model.City,
                Street = model.Street,
                ZipCode = model.ZipCode,
            };
                

            await _userRepository.CreateAsync(user);
            await _userProfileRepository.CreateAsync(userProfile);

            var result = Authenticate(user);

            return new Result<ClaimsIdentity>()
            {
                Data = result,
            };
        }

        private bool IsVerifyPassword(string userPasswordHash, string userPassword)
        {
            var hash = HashPasswordHelper.HashPassword(userPassword);
            return userPasswordHash == hash;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
