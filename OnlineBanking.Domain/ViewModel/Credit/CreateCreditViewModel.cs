using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    public class CreateCreditViewModel
    {
        public decimal CreditSumAmount { get; set; }

        public float Percent { get; set; }

        public DateTime CreditTerm { get; set; }
    }
}
