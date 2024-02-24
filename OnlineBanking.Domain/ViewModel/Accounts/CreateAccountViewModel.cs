using OnlineBanking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    public class CreateAccountViewModel
    {
        public string AccountName { get; set; }

        public AccountType AccountType { get; set; }
    }
}
