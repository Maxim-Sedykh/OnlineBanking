using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface ICreditTypeValidator: IBaseValidator<CreditType>
    {
        /// <summary>
        /// Проверяет на null типы кредита
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Result.Result ValidateCreditTypesOnNull<T>(IEnumerable<T> models);
    }
}
