using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using OnlineBanking.Domain.Result;
using OnlineBanking.Domain.ViewModel.AccountType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface IAccountTypeValidator: IBaseValidator<AccountType>
    {
        /// <summary>
        /// Проверяет на null данные счета
        /// </summary>
        /// <param name="accountTypes"></param>
        /// <returns></returns>
        Result.Result ValidateAccountTypesOnNull(IEnumerable<SelectAccountTypeViewModel> accountTypes);
    }
}
