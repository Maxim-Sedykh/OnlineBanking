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

        /// <summary>
        /// Номер банковской карты
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Карта годна до
        /// </summary>
        public DateTime Validity { get; set; }

        /// <summary>
        /// CVV банковской карты
        /// </summary>
        public string CVV { get; set; }

        /// <summary>
        /// Внешний ключ для связи с счётом один к одному
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Счёт карты
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Тип карты (Кредитная, Дебетовая)
        /// </summary>
        public CardType CardType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
