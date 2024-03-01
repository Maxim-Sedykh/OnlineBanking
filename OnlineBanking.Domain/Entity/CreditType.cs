using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class CreditType : IEntityId<byte>, IAuditable
    {
        public byte Id { get; set; }

        public string CreditTypeName { get; set; }

        public string Description { get; set; }

        public short MinCreaditTermInMonths { get; set; }

        public short MaxCreaditTermInMonths { get; set; }

        public float YearPercent { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public List<Credit> Credits { get; set; }
    }
}
