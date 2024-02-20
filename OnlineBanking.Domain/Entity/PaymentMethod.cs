using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class PaymentMethod : IEntityId<byte>, IAuditable
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
