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
    public class CreditTypeValidator : ICreditTypeValidator
    {
        public Result ValidateCreditTypesOnNull<T>(IEnumerable<T> creditTypes)
        {
            if (creditTypes == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.CreditTypesNotFound,
                    ErrorMessage = ErrorMessage.CreditTypesNotFound,
                };
            }
            return new Result();
        }

        public Result ValidateEntityOnNull(CreditType model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.CreditTypeNotFound,
                    ErrorMessage = ErrorMessage.CreditTypeNotFound,
                };
            }
            return new Result();
        }
    }
}
