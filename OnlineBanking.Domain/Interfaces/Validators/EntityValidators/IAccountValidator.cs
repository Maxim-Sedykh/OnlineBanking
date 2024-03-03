using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface IAccountValidator: IBaseValidator<Account>
    {
        /// <summary>
        /// Проверяется наличие товара, если товар с переданным названием есть в БД, то создать точно такой же нельзя
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Result.Result CreateValidate(Account account);

        /// <summary>
        /// Проверяет, равен ли баланс счёта нулю, если баланс не равен нулю, то счёт нельзя удалить
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Result.Result ValidateBalance(decimal accountBalance);
    }
}
