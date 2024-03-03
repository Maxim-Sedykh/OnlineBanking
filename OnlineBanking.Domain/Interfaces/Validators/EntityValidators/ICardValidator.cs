using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Validators.BaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Validators.EntityValidators
{
    public interface ICardValidator: IBaseValidator<Card>
    {
        /// <summary>
        /// Проверяет, есть ли банковские карты с таким же номером, как текущая создаваемая карта
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Result.Result ValidateCardNumber(Card card);
    }
}
