using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Models;
using System.Diagnostics;

namespace OnlineBanking.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {

        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
