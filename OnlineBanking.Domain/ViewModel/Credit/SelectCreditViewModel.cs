using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    public class SelectCreditViewModel
    {
        public long Id { get; set; }

        public string CreditTypeName { get; set; }

        public decimal CreditRemainerAmount { get; set; }

        public decimal MonthlyPayment { get; set; }

        public DateTime CreditTerm { get; set; }
    }
}
