using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Enum
{
    public enum AccountType
    {
        [Display(Name = "Текущий счёт")]
        Current = 0,

        [Display(Name = "Сберегательный счёт")]
        Savings = 1,

        [Display(Name = "Депозитный счёт")]
        Deposit = 2,
    }
}
