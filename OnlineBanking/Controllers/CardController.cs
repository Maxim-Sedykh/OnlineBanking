using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Auth;
using System.Security.Claims;
using System.Diagnostics.Contracts;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.ViewModel.Card;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using OnlineBanking.Domain.Extensions;

namespace OnlineBanking.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Создание банковской карты для определённого счёта по идентификатору (POST) 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCardForAccount(long id)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _cardService.CreateCardForAccount(id);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Переход для модального окна для того, чтобы посмотреть данные банковской карты (GET)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCardByAccountId(long id)
        {
            var response = await _cardService.GetCardByAccountId(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }
    }
}
