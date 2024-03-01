using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Auth;
using OnlineBanking.Domain.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    /// <summary>
    /// Данный сервис предназначен для работы с транзакциями
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Получение заполнителей для счёта пользователя и платёжных методов для создания транзакции
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<CreateTransactionViewModel>> GetDataToMakeTransaction(string userName);

        /// <summary>
        /// Совершение транзакции
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<Transaction>> MakeTransaction(CreateTransactionViewModel viewModel, string userName);

        /// <summary>
        /// Получение транзакций пользователя, заполнение информации для страницы с транзакциями
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<TransactionPageViewModel>> GetUserTransactions(string userName);

        /// <summary>
        /// Совершение выплаты по кредиту
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<Transaction>> MakeCreditTransaction(CreateTransactionViewModel viewModel, string userName);
        
    }
}
