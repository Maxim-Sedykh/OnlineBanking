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

        /// <summary>
        /// Название типа счёта
        /// </summary>
        public string AccountTypeName { get; set; }

        /// <summary>
        /// Описание типа счёта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Годовая процентная ставка типа кредита
        /// </summary>
        public float AnnualInterestRate { get; set; }

        /// <summary>
        /// Счета, созданные по определённому типу счёта
        /// </summary>
        public List<Account> Accounts { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
