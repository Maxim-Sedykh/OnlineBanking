using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.CreditType
{
    /// <summary>
    /// Модель представления для поля для выбора типа кредита при его создании
    /// </summary>
    public record SelectCreditTypeViewModel
    {
        public byte Id { get; set; }

        public string CreditTypeName { get; set; }
    }
}
