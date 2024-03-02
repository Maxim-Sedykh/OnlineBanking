using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Models;
using System.Diagnostics;
using System.Security.Claims;
using OnlineBanking.Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;

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
        /// Редактировать данные пользователя (PUT)
        /// </summary>
        /// <param name="viemModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditUserData(UserProfileViewModel viemModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("CreatedAt");
            if (ModelState.IsValid)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(viemModel.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)viemModel.Avatar.Length);
                }
                await _userProfileService.EditUserInfo(viemModel, imageData);
            }
            return RedirectToAction("UserProfile");
        }
    }
}
