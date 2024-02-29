using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace OnlineBanking.Controllers
{
    public class CreditController : Controller
    {
        public CreditController()
        {
                
        }

        [HttpGet]
        public IActionResult GetCreditData() => View();
    }
}
