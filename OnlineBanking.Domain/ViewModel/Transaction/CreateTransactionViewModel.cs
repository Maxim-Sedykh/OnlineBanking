﻿using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.ViewModel.Accounts;
using OnlineBanking.Domain.ViewModel.Credit;
using OnlineBanking.Domain.ViewModel.CreditType;
using OnlineBanking.Domain.ViewModel.PaymentMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Transaction
{
    /// <summary>
    /// Модель представления для создания транзакций разных типов
    /// </summary>
    public record CreateTransactionViewModel
    {
        [Required(ErrorMessage = "Карта должна быть обязательно указана")]
        [MaxLength]
        public string RecipientCardNumber { get; set; }

        public decimal MoneyAmount { get; set; }

        public byte SelectedPaymentMethodId { get; set; }

        public List<SelectPaymentMethodViewModel> PaymentMethodNames { get; set; }

        public long SelectedUserAccountId { get; set; }

        public List<AccountMoneyViewModel> UserAccounts { get; set; }

        public long SelectedUserCreditId { get; set; }

        public List<SelectCreditViewModel> UserCredits { get; set; }
    }
}
