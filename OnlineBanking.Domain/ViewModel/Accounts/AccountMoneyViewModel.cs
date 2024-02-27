using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    public class AccountMoneyViewModel
    {
        public long Id { get; set; }

        public string AccountName { get; set; }

        public decimal BalanceAmount { get; set; }
    }
}
