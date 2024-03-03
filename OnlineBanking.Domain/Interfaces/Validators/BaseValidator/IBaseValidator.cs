using OnlineBanking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.BaseValidator
{
    public interface IBaseValidator<in T> where T : class
    {
        /// <summary>
        /// Проверяет, равна ли сущность null
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Result.Result ValidateEntityOnNull(T model);
    }
}
