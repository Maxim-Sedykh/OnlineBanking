using OnlineBanking.Domain.Result;
using OnlineBanking.Security.UserTokenService.TokenViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IUserTokenService
    {
        Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims);

        Task<string> GenerateRefreshTokenAsync();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

        Task<Result<UserTokenViewModel>> RefreshTokenAsync(UserTokenViewModel dto);
    }
}
