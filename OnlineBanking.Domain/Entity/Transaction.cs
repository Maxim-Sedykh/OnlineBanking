using OnlineBanking.Domain.Interfaces.Entity;
using OnlineBanking.Domain.ValueObjects.Transaction;
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

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public Money Money { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
