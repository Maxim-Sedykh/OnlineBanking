using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Credit;
using System.Reflection.Metadata.Ecma335;

namespace OnlineBanking.Controllers
{
    public class CreditController : Controller
    {
        private readonly ICreditService _creditService;

        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCreditTypes()
        {
            var response = await _creditService.GetCreditTypes();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCredit()
        {
            var response = await _creditService.GetCreditTypesToSelect();
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCredit(CreateCreditViewModel viewModel)
        {
            var response = await _creditService.CreateCredit(viewModel, User.Identity.Name);
            if (response.IsSuccess)
            {
                return RedirectToAction("GetCreditInfo", "Credit");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> SetUserIncome(SetIncomeViewModel viewModel)
        {
            var response = await _creditService.SetUserIncome(viewModel, User.Identity.Name);
            if (response.IsSuccess)
            {
                return RedirectToAction("GetCreditInfo", "Credit");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpGet]
        public async Task<IActionResult> GetCreditInfo()
        {
            var response = await _creditService.GetIncomeViewModel(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCredits()
        {
            var response = await _creditService.GetUserCredits(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }
    }
}
