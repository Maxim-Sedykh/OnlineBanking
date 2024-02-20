using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Result;
using OnlineBanking.Security.UserTokenService.TokenViewModels;

namespace OnlineBanking.Controllers
{
    public class UserTokenController : Controller
    {
        private readonly IUserTokenService _userTokenService;

        public UserTokenController(IUserTokenService userTokenService)
        {
            _userTokenService = userTokenService;
        }

        /// <summary>
        /// Обновление токена пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [Route("refresh")]
        [HttpPost]
        public async Task<ActionResult<Result<UserTokenViewModel>>> RefreshToken([FromBody] UserTokenViewModel model)
        {
            var response = await _userTokenService.RefreshTokenAsync(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        
    }
}
