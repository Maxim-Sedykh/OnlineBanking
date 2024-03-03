using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Interfaces.Services;
using System.Diagnostics;
using System.Security.Claims;
using OnlineBanking.Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace OnlineBanking.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userService)
        {
            _userProfileService = userService;
        }

        /// <summary>
        /// Переход на страницу профиля пользователя (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var response = await _userProfileService.GetUserProfile(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        /// <summary>
        /// Редактировать данные пользователя (Post)
        /// </summary>
        /// <param name="viemModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditUserData(UserProfileViewModel viemModel)
        {
            
            await _userProfileService.EditUserInfo(viemModel);   
            return RedirectToAction("GetUserProfile");
        }
    }
}
