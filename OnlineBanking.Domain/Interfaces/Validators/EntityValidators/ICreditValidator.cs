using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface ICreditValidator: IBaseValidator<Credit>
    {
        /// <summary>
        /// Проверяет правильность срока кредита
        /// </summary>
        /// <param name="creditTerm"></param>
        /// <param name="creditType"></param>
        /// <returns></returns>
        Result.Result ValidateCreditByTerm(DateTime creditTerm, CreditType creditType);

        /// <summary>
        /// Проверяет возможность взять кредит пользователю, исходя из его дохода
        /// </summary>
        /// <param name="userIncome"></param>
        /// <param name="userMonthlyCreditPayment"></param>
        /// <returns></returns>
        Result.Result ValidateCreditVerify(decimal userIncome, decimal userMonthlyCreditPayment);
    }
}
