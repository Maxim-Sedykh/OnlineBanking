using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    public class CreditViewModel
    {
        public long Id { get; set; }

        public decimal CreditSumAmount { get; set; }

        public decimal MoneyLenderReceiveAmount { get; set; }

        public float Percent { get; set; }

        public DateTime CreditTerm { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal CreditRemainerAmount { get; set; }
    }
}
