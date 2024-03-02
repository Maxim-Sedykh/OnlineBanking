using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Credit : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Сколько пользователю необходимо выплатить по кредиту исходя из процентов
        /// </summary>
        public decimal CreditSumAmount { get; set; }

        /// <summary>
        /// Сколько денег получит пользователь от кредита
        /// </summary>
        public decimal MoneyLenderReceiveAmount { get; set; }

        /// <summary>
        /// Крайний срок погашения кредита
        /// </summary>
        public DateTime CreditTerm { get; set; }

        /// <summary>
        /// Остаток по кредиту, который необходимо выплатить
        /// </summary>
        public decimal CreditRemainerAmount { get; set; }

        /// <summary>
        /// Ежемесячный платёж по кредиту
        /// </summary>
        public decimal MonthlyPayment { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом кредита
        /// </summary>
        public byte CreditTypeId { get; set; }

        /// <summary>
        /// Тип кредита
        /// </summary>
        public CreditType CreditType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
