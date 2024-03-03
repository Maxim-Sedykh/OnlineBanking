using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface IUserValidator: IBaseValidator<User>
    {
        /// <summary>
        /// Проверяет правильность срока кредита
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result.Result RegisterUserValidate(User user);
    }
}
