using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineBanking.Application.Resources;
using OnlineBanking.DAL.Interfaces.Repositories;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.Settings;
using OnlineBanking.Security.UserTokenService.TokenViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public UserTokenService(IOptions<JwtSettings> options, IBaseRepository<User> userRepository)
        {
            _jwtKey = options.Value.JwtKey;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(15), credentials);
            var token = _tokenHandler.WriteToken(securityToken);
            return await Task.FromResult(token);
        }

        public async Task<string> GenerateRefreshTokenAsync()
        {
            var randomNumbers = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumbers);
            return await Task.FromResult(Convert.ToBase64String(randomNumbers));
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer,

            };
            var claimsPrincipal = _tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SecurityTokenException(ErrorMessage.InvalidToken);
            }
            return claimsPrincipal;
        }

        public async Task<Result<UserTokenViewModel>> RefreshTokenAsync(UserTokenViewModel dto)
        {
            string accessToken = dto.AccessToken;
            string refreshToken = dto.RefreshToken;

            var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);
            var userName = claimsPrincipal.Identity?.Name;

            var user = await _userRepository.GetAll()
                .Include(x => x.UserToken)
                .FirstOrDefaultAsync(x => x.Username == userName);
            if (user == null || user.UserToken.RefreshToken != refreshToken ||
                user.UserToken.RefreshTokenExpireTime <= DateTime.UtcNow)
            {
                return new Result<UserTokenViewModel>()
                {
                    ErrorMessage = ErrorMessage.InvalidClientRequest,
                };
            }

            var newAccessToken = await GenerateAccessTokenAsync(claimsPrincipal.Claims);
            var newRefreshToken = await GenerateRefreshTokenAsync();

            user.UserToken.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user);

            return new Result<UserTokenViewModel>()
            {
                Data = new UserTokenViewModel()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                }
            };
        }
    }
}
