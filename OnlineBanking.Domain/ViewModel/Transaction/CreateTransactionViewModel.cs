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
        public string RecipientCardNumber { get; set; }

        public decimal MoneyAmount { get; set; }

        public byte SelectedPaymentMethodId { get; set; }

        public List<SelectPaymentMethodViewModel> PaymentMethodNames { get; set; }

        public long SelectedUserAccountId { get; set; }

        public List<AccountMoneyViewModel> UserAccounts { get; set; }
    }
}
