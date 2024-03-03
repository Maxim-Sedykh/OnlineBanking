using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Credit;

namespace OnlineBanking.Controllers
{
    public class CreditController : Controller
    {
        private readonly ICreditService _creditService;

        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        /// <summary>
        /// Переход на страницу для получения информации по типам кредита (GET)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Переход на модальное окно для создания кредита (GET)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Создание кредита (POST)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Выставить доход пользователя (POST)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Переход на страницу для получения информации по кредитам пользователя (GET)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение кредитов пользователя (GET)
        /// </summary>
        /// <returns></returns>
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