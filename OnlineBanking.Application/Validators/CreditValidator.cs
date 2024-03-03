using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Validators
{
    public class CreditValidator : ICreditValidator
    {
        public Result ValidateCreditByTerm(DateTime creditTerm, CreditType creditType)
        {
            if (creditTerm > DateTime.UtcNow.AddMonths(creditType.MaxCreaditTermInMonths) ||
                creditTerm < DateTime.UtcNow.AddMonths(creditType.MinCreaditTermInMonths))
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.InvalidCreditTerm,
                    ErrorMessage = ErrorMessage.InvalidCreditTerm,
                };
            }

            return new Result();
        }

        public Result ValidateCreditVerify(decimal userIncome, decimal userMonthlyCreditPayment)
        {
            decimal maxLivingWage = 20000;

            if (userIncome - userMonthlyCreditPayment < maxLivingWage)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.CreditNotApproved,
                    ErrorMessage = ErrorMessage.CreditNotApproved,
                };
            }

            return new Result();
        }

        public Result ValidateEntityOnNull(Credit model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.CreditNotFound,
                    ErrorMessage = ErrorMessage.CreditNotFound,
                };
            }
            return new Result();
        }
    }
}
