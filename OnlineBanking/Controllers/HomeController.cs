using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OnlineBanking.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Переход на главную страницу (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Переход на страницу с ошибкой
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string ErrorMessage)
        {
            return View(ErrorMessage);
        }
    }
}
