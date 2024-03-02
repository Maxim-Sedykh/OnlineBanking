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

        /// <summary>
        /// Название типа кредита
        /// </summary>
        public string CreditTypeName { get; set; }

        /// <summary>
        /// Описание типа кредита, для чего берётся
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Минимальное количество месяцев в качестве срока выплаты кредита
        /// </summary>
        public short MinCreaditTermInMonths { get; set; }

        /// <summary>
        /// Максимальное количество месяцев в качестве срока выплаты кредита
        /// </summary>
        public short MaxCreaditTermInMonths { get; set; }

        /// <summary>
        /// Годовой процент по выплате кредита
        /// </summary>
        public float YearPercent { get; set; }

        /// <summary>
        /// Кредиты, которые были созданы под определённым типом
        /// </summary>
        public List<Credit> Credits { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
