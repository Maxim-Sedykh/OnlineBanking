using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OnlineBanking.Controllers
{
    [Authorize]
    public class TransactionController: Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Переход на страницу с информацией о транзакциях пользователя (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserTransaction()
        {
            var response = await _transactionService.GetUserTransactions(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Переход на модальное окно для создания транзакций разного типа (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
        {
            var response = await _transactionService.GetDataToMakeTransaction(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Создание обычной транзакции (POST)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание кредитной транзакции (POST)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCreditTransaction(CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _transactionService.MakeCreditTransaction(viewModel, User.Identity.Name);
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
