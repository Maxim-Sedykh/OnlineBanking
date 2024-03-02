using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Transaction : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем отправителем
        /// </summary>
        public long SenderId { get; set; }

        /// <summary>
        /// Отправитель транзакции
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем получателем
        /// </summary>
        public long RecipientId { get; set; }

        /// <summary>
        /// Получатель транзакции
        /// </summary>
        public User Recipient { get; set; }

        /// <summary>
        /// Количество средств, которые были переведены
        /// </summary>
        public decimal MoneyAmount { get; set; }

        /// <summary>
        /// Внешний ключ для связи с платежным методом
        /// </summary>
        public byte PaymentMethodId { get; set; }

        /// <summary>
        /// Тип транзакции (Обычная, Кредитная)
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Платежный метод транзакции
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
