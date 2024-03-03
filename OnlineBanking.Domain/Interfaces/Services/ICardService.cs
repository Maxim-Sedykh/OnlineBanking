using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, который отвечает за работу с банковскими картами, которые привязываются к определённому счёту
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Получить карту счёта по идентификатору аккаунта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<CardViewModel>> GetCardByAccountId(long id);

        /// <summary>
        /// Создание банковской карты для определённого счёта по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result.Result> CreateCardForAccount(long id);
    }
}
