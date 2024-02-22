using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Transaction
{
    public class TransactionViewModel
    {
        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public decimal MoneyAmount { get; set; }

        public string PaymentMethodName { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
