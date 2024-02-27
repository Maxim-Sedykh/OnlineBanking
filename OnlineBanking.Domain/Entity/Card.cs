using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Card:IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime Validity { get; set; }

        public string CVV { get; set; }

        public long AccountId { get; set; }

        public Account Account { get; set; }

        public CardType CardType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
