using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    /// <summary>
    /// Модель представления для удаления счёта
    /// </summary>
    public record AccountDeleteViewModel
    {
        public long Id { get; set; }

        public decimal BalanceAmount { get; set; }
    }
}
