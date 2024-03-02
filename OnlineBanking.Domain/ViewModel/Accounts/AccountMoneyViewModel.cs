using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    /// <summary>
    /// Модель представления для различных методов, предосталяет информацию о денежных средствах на счётах
    /// </summary>
    public record AccountMoneyViewModel
    {
        public long Id { get; set; }

        public string AccountName { get; set; }

        public decimal BalanceAmount { get; set; }
    }
}
