﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBanking.Application.Services;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Extensions;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Auth;
using OnlineBanking.Domain.ViewModel.Error;
using System.Net;

namespace OnlineBanking.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Переход на модальное окно, для того, чтобы положить деньги на счёт (GET)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddMoneyToAccount(int id)
        {
            var response = await _accountService.GetAccountById(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        /// <summary>
        /// Пополнить баланс счёта (POST)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMoneyToAccount(AccountMoneyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _accountService.AddMoneyToAccount(viewModel);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Переход на модальное окно, для того, чтобы создать счёт (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateAccount()
        {
            var response = await _accountService.GetAccountTypeNames();
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        /// <summary>
        /// Создание счёта (POST)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _accountService.CreateNewAccount(viewModel, User.Identity.Name);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Удаление счёта, если баланс счёта пуст (POST)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAccountById(AccountDeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _accountService.DeleteAccountById(model);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Переход на страницу для получения информации по типам счётов (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAccountTypes()
        {
            var response = await _accountService.GetAccountTypes();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.ErrorMessage }");
        }

        /// <summary>
        /// Выплата процентов пользователям
        /// </summary>
        /// <returns></returns>
        public async Task SetAccountPercent()
        {
            await _accountService.PayAccountsPercent();
        }
    }
}
