using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Validators
{
    public class PaymentMethodValidator : IPaymentMethodValidator
    {
        public Result ValidateEntityOnNull(PaymentMethod model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.PaymentMethodNotFound,
                    ErrorMessage = ErrorMessage.PaymentMethodNotFound,
                };
            }
            return new Result();
        }

        public Result ValidatePaymentMethodsOnNull(IEnumerable<PaymentMethod> paymentMethods)
        {
            if (paymentMethods == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.PaymentMethodsNotFound,
                    ErrorMessage = ErrorMessage.PaymentMethodsNotFound,
                };
            }
            return new Result();
        }
    }
}
