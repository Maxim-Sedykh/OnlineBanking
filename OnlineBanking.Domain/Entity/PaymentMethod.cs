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

        /// <summary>
        /// Название платёжного метода
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание платёжного метода
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Транзакции, которые были совершены данным платежным методом
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
