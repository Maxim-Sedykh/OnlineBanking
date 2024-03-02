using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.AccountType
{
    /// <summary>
    /// Модель представления для поля выбора типа счёта
    /// </summary>
    public record SelectAccountTypeViewModel
    {
        public byte Id { get; set; }

        public string AccountTypeName { get; set; }
    }
}
