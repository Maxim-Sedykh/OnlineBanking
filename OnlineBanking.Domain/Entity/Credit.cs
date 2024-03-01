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

        public decimal CreditSumAmount { get; set; }

        public decimal MoneyLenderReceiveAmount { get; set; }

        public DateTime CreditTerm { get; set; }

        public decimal CreditRemainerAmount { get; set; }

        public decimal MonthlyPayment { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public byte CreditTypeId { get; set; }

        public CreditType CreditType { get; set; }
    }
}
