using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBanking.Application.Resources;
using OnlineBanking.DAL.Repositories;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Auth;
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
        private readonly ILogger _logger;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<ClaimsIdentity>> Login(LoginUserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    return new Result<ClaimsIdentity>()
                    {
                        ErrorMessage = ErrorMessage.UserNotFound,
                        ErrorCode = (int)StatusCode.UserNotFound,
                    };
                }

                if (!IsVerifyPassword(user.Password, model.Password))
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
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)StatusCode.InternalServerError
                };
            }
        }

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

            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.Username);
                if (user != null)
                {
                    return new Result<ClaimsIdentity>()
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
                    City = model.City,
                    Street = model.Street,
                    ZipCode = model.ZipCode,
                    CreatedAt = DateTime.UtcNow,
                };
                await _userRepository.CreateAsync(user);

                var result = Authenticate(user);

                return new Result<ClaimsIdentity>()
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new Result<ClaimsIdentity>()
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

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
