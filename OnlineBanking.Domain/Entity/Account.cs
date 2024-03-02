using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Account : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        /// <summary>
        /// Имя счёта
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом счёта
        /// </summary>
        public byte AccountTypeId { get; set; }

        /// <summary>
        /// Тип счёта
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public decimal BalanceAmount { get; set; }

        /// <summary>
        /// Привязана ли банковская карта к счёту
        /// </summary>
        public bool IsCardLinked { get; set; }

        /// <summary>
        /// Карта счёта
        /// </summary>
        public Card Card { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
