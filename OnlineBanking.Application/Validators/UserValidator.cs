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
    public class UserValidator : IUserValidator
    {
        public Result RegisterUserValidate(User user)
        {
            if (user != null)
            {
                return new Result()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExist,
                    ErrorCode = (int)StatusCode.UserAlreadyExist
                };
            }

            return new Result();
        }

        public Result ValidateEntityOnNull(User model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }
            return new Result();
        }
    }
}
