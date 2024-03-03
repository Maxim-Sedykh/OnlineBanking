using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface ITransactionValidator
    {
        /// <summary>
        /// Проверяет счёт пользователя на достаточное количество баланса для транзакции
        /// </summary>
        /// <param name="accountMoney"></param>
        /// <param name="moneyAmountTransaction"></param>
        /// <returns></returns>
        Result.Result ValidateAvailabilityAccountBalance(decimal accountMoney, decimal moneyAmountTransaction);
    }
}
