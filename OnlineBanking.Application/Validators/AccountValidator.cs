using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Validators
{
    public class AccountValidator : IAccountValidator
    {
        public Result CreateValidate(Account account)
        {
            if (account != null)
            {
                return new Result()
                {
                    ErrorMessage = ErrorMessage.AccountAlreadyExist,
                    ErrorCode = (int)StatusCode.AccountAlreadyExist
                };
            }

            return new Result();
        }

        public Result ValidateBalance(decimal accountBalance)
        {
            if (accountBalance != 0)
            {
                return new Result()
                {
                    ErrorMessage = ErrorMessage.AccountBalanceNotEmpty,
                    ErrorCode = (int)StatusCode.AccountBalanceNotEmpty
                };
            }

            return new Result();
        }

        public Result ValidateEntityOnNull(Account model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.AccountNotFound,
                    ErrorMessage = ErrorMessage.AccountNotFound,
                };
            }
            return new Result();
        }
    }
}
