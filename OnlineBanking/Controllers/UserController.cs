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
    public class UserController : Controller
    {
        [Authorize]
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var response = await _userService.GetUserProfile(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
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
                await _userService.EditUserInfo(viemModel, imageData);
            }
            return RedirectToAction("UserProfile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
