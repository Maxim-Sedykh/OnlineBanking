using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Enum
{
    public enum TransactionType
    {
        [Display(Name = "Обычная транзакция")]
        Common = 0,

        [Display(Name = "Кредитная транзакция")]
        Credit = 1,
    }
}
