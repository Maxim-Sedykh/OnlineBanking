using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Transaction
{
    public class CreateTransactionViewModel
    {
        public long Id { get; set; }

        public string RecipientCardNumber { get; set; }

        public decimal MoneyAmount { get; set; }

        public string SelectedPaymentMethodName { get; set; }

        public List<string> PaymentMethodNames { get; set; }

        public string SelectedUserAccount { get; set; }

        public List<AccountMoneyViewModel> UserAccounts { get; set; }
    }
}
