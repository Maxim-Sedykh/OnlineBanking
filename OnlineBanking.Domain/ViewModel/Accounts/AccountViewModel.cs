using OnlineBanking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    public class AccountViewModel
    {
        public long Id { get; set; }

        public string AccountName { get; set; }

        public string AccountTypeName { get; set; }

        public decimal BalanceAmount { get; set; }

        public bool IsCardLinked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
