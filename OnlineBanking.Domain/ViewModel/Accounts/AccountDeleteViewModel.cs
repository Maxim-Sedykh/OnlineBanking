using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    public class AccountDeleteViewModel
    {
        public int Id { get; set; }

        public decimal BalanceAmount { get; set; }
    }
}
