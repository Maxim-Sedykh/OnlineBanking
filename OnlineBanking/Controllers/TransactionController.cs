using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;

namespace OnlineBanking.Controllers
{
    public class TransactionController: Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetUserTransaction()
        {
            return View();
        }
    }
}
