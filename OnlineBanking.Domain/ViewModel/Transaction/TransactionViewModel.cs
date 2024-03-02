using OnlineBanking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Transaction
{
    /// <summary>
    /// Модель представления для получения подробной информации по транзакциям
    /// </summary>
    public record TransactionViewModel
    {
        public long Id { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public decimal MoneyAmount { get; set; }

        public string PaymentMethodName { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
