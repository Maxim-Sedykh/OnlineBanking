using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Auth;

namespace OnlineBanking.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> AddMoneyToAccount(int id)
        {
            var response = await _accountService.GetAccountById(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> AddMoneyToAccount(AccountMoneyViewModel viewModel)
        {
            var response = await _accountService.AddMoneyToAccount(viewModel);
            if (response.IsSuccess)
            {
                return RedirectToAction("UserProfile", "User");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpGet]
        public async Task<IActionResult> CreateAccount()
        {
            var response = await _accountService.GetAccountTypeNames();
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel viewModel)
        {
            var response = await _accountService.CreateNewAccount(viewModel, User.Identity.Name);
            if (response.IsSuccess)
            {
                return RedirectToAction("UserProfile", "User");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccountById([FromBody] AccountDeleteViewModel model)
        {
            var response = await _accountService.DeleteAccountById(model);
            if (response.IsSuccess)
            {
                return Ok(new { message = response.SuccessMessage });
            }
            return BadRequest(new { message = response.ErrorMessage });
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountTypes()
        {
            var response = await _accountService.GetAccountTypes();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }
    }
}
