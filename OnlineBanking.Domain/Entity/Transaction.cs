using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Transaction : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public long SenderId { get; set; }

        public User Sender { get; set; }

        public long RecipientId { get; set; }

        public User Recipient { get; set; }

        public decimal MoneyAmount { get; set; }

        public byte PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
