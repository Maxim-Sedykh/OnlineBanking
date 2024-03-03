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
    public class TransactionValidator : ITransactionValidator
    {
        public Result ValidateAvailabilityAccountBalance(decimal accountMoney, decimal moneyAmountTransaction)
        {
            if (accountMoney < moneyAmountTransaction)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.NotEnoughFunds,
                    ErrorMessage = ErrorMessage.NotEnoughFunds,
                };
            }

            return new Result();
        }
    }
}
