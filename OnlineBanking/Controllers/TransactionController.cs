using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Transaction;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<IActionResult> GetUserTransaction()
        {
            var response = await _transactionService.GetUserTransactions(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
        {
            var response = await _transactionService.GetDataToMakeTransaction(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _transactionService.MakeTransaction(viewModel, User.Identity.Name);
                if (response.IsSuccess)
                {
                    return Ok(new { message = response.SuccessMessage });
                }
                return BadRequest(new { message = response.ErrorMessage });
            }
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToArray();
            string errorMessage = string.Join(" ", errorMessages);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = errorMessage });
        }
    }
}
