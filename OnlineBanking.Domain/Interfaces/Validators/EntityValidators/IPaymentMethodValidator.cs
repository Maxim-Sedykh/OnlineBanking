using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface IPaymentMethodValidator: IBaseValidator<PaymentMethod>
    {
        /// <summary>
        /// Проверяет на null данные платёжные методы
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        Result.Result ValidatePaymentMethodsOnNull(IEnumerable<PaymentMethod> paymentMethods);
    }
}
