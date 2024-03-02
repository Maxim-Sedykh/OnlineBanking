using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    /// <summary>
    /// Модель представления для предоставления информации о доходе пользователя и о том, подтвержён ли доход
    /// </summary>
    public record SetIncomeViewModel
    {
        [Required(ErrorMessage = "Укажите ваш доход")]
        public decimal UserIncome { get; set; }

        public bool IsIncomeVerified { get; set; }
    }
}
