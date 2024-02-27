using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class AccountType : IEntityId<byte>, IAuditable
    {
        public byte Id { get; set; }

        public string AccountTypeName { get; set; }

        public string Description { get; set; }

        public float AnnualInterestRate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
