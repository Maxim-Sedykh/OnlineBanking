﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Auth;
using System.Security.Claims;
using System.Diagnostics.Contracts;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.ViewModel.Card;

namespace OnlineBanking.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCardForAccount([FromBody] long accountId)
        {
            var response = await _cardService.CreateCardForAccount(accountId);
            if (response.IsSuccess)
            {
                return Ok(new { message = response.SuccessMessage });
            }
            return BadRequest(new { message = response.ErrorMessage });
        }

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
