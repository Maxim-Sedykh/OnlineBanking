using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Credit : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public decimal LoanSumAmount { get; set; }

        public float Percent { get; set; }

        public DateTime CreditDate { get; set; }

        public DateTime CreditTerm { get; set; }

        public decimal LoanRemainerAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
