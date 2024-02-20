using OnlineBanking.Domain.Result;
using OnlineBanking.Security.UserTokenService.TokenViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Security.UserTokenService.Interface
{
    public interface IUserTokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

        Task<Result<UserTokenViewModel>> RefreshToken(UserTokenViewModel dto);
    }
}
