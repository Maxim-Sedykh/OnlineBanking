using OnlineBanking.Domain.Interfaces.Entity;
using OnlineBanking.Domain.ValueObjects.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Loan : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public Money LoanSum { get; set; }

        public float Percent { get; set; }

        public DateTime CreditTerm { get; set; }

        public Money LoanRemainder { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
