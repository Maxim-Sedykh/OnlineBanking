using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.AccountType;
using OnlineBanking.Domain.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, отвечающий за работу с счетами пользователя
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Удаление счёта по идентификатору, удаление осуществляется, если у пользователя баланс равен нулю
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Result<bool>> DeleteAccountById(AccountDeleteViewModel viewModel);

        /// <summary>
        /// Создаёт новый счёт с нулевым балансом
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Result<Account>> CreateNewAccount(CreateAccountViewModel viewModel, string username);

        /// <summary>
        /// Добавление денег на счёт
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Result<Account>> AddMoneyToAccount(AccountMoneyViewModel viewModel);

        /// <summary>
        /// Получение счёта по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<AccountMoneyViewModel>> GetAccountById(int id);

        /// <summary>
        /// Получение списка типов счётов
        /// </summary>
        /// <returns></returns>
        Task<Result<CreateAccountViewModel>> GetAccountTypeNames();

        /// <summary>
        /// Получение списка типов счёта с подробной информацией
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<AccountTypeViewModel>> GetAccountTypes();
    }
}
