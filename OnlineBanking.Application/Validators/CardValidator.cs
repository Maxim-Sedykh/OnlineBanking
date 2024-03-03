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
    public class CardValidator : ICardValidator
    {
        public Result ValidateCardNumber(Card card)
        {
            if (card != null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.TryAgain,
                    ErrorMessage = ErrorMessage.TryAgain,
                };
            }
            return new Result();
        }

        public Result ValidateEntityOnNull(Card model)
        {
            if (model == null)
            {
                return new Result()
                {
                    ErrorCode = (int)StatusCode.CardNotFound,
                    ErrorMessage = ErrorMessage.CardNotFound,
                };
            }
            return new Result();
        }
    }
}
