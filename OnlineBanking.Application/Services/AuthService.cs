using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.DAL.Interfaces.Repositories;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ValueObjects.User;
using OnlineBanking.Domain.ViewModel.Auth;
using OnlineBanking.Security.UserTokenService.TokenViewModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace OnlineBanking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<UserToken> _userTokenRepository;
        private readonly IUserTokenService _userTokenService;
        private readonly ILogger _logger;

        public AuthService(IBaseRepository<User> userRepository, IBaseRepository<UserToken> userTokenRepository, IUserTokenService userTokenService, ILogger logger)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _userTokenService = userTokenService;
            _logger = logger;
        }

        public async Task<Result<UserTokenViewModel>> Login(LoginUserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.UserName);
                if (user == null)
                {
                    return new Result<UserTokenViewModel>()
                    {
                        ErrorMessage = ErrorMessage.UserNotFound,
                        ErrorCode = (int)StatusCode.UserNotFound,
                    };
                }

                if (!IsVerifyPassword(user.Password, model.Password))
                {
                    return new Result<UserTokenViewModel>()
                    {
                        ErrorMessage = ErrorMessage.PasswordIsWrong,
                        ErrorCode = (int)StatusCode.PasswordIsWrong,
                    };
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var accessToken = await _userTokenService.GenerateAccessTokenAsync(claims);

                var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
                var refreshToken = await _userTokenService.GenerateRefreshTokenAsync();

                if (userToken == null)
                {
                    userToken = new UserToken
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken,
                        RefreshTokenExpireTime = DateTime.UtcNow.AddDays(5)
                    };

                    await _userTokenRepository.CreateAsync(userToken);
                }
                else
                {
                    userToken.RefreshToken = refreshToken;
                    userToken.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(5);

                    await _userTokenRepository.UpdateAsync(userToken);
                }

                return new Result<UserTokenViewModel>()
                {
                    Data = new UserTokenViewModel()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<UserTokenViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError
                };
            }
        }

        public async Task<Result<UserViewModel>> Register(RegisterUserViewModel model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                return new Result<UserViewModel>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)StatusCode.PasswordNotEqualsPasswordConfirm,
                };
            }

            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.Username);
                if (user != null)
                {
                    return new Result<UserViewModel>()
                    {
                        ErrorMessage = ErrorMessage.UserAlreadyExist,
                        ErrorCode = (int)StatusCode.UserAlreadyExist,
                    };
                }
                var hashUserPassword = HashPassword(model.Password);
                user = new User()
                {
                    Username = model.Username,
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Email = model.Email,
                    IsOnlineBankingUser = true,
                    Password = hashUserPassword,
                    Role = Role.User,
                    Address = model.Address,
                    CreatedAt = DateTime.UtcNow,
                };
                await _userRepository.CreateAsync(user);

                return new Result<UserViewModel>()
                {
                    Data = user.Adapt<UserViewModel>(),
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<UserViewModel>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError
                };
            }
        }

        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool IsVerifyPassword(string userPasswordHash, string userPassword)
        {
            var hash = HashPassword(userPassword);
            return userPasswordHash == hash;
        }
    }
}
