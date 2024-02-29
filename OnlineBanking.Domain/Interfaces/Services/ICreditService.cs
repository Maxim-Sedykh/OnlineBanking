using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Card;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.CreditType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис предназначенный для работы с кредитами
    /// </summary>
    public interface ICreditService
    {
        /// <summary>
        /// Получение информации о типах кредита
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<CreditTypeViewModel>> GetCreditTypes();

        /// <summary>
        /// Создание кредита для пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Result<CreditViewModel>> CreateCredit(CreateCreditViewModel viewModel, string userName);

        /// <summary>
        /// Получение кредитов пользователя
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<CreditViewModel>> GetUserCredits(string userName);

        /// <summary>
        /// Выставление дохода пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Result<User>> SetUserIncome(SetIncomeViewModel viewModel, string userName);
    }
}
