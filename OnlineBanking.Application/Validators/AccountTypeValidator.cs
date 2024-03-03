using OnlineBanking.Application.Resources.Error;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.AccountType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Validators
{
    public class AccountTypeValidator : IAccountTypeValidator
    {
        public Result ValidateAccountTypesOnNull(IEnumerable<SelectAccountTypeViewModel> accountTypes)
        {
            if (accountTypes == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.AccountTypesNotFound,
                    ErrorMessage = ErrorMessage.AccountTypesNotFound,
                };
            }
            return new Result();
        }

        public Result ValidateEntityOnNull(AccountType model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.AccountTypeNotFound,
                    ErrorMessage = ErrorMessage.AccountTypeNotFound,
                };
            }
            return new Result();
        }
    }
}
