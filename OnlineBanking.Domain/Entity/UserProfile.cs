using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class UserProfile : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Сколько пользователь платит в месяц за свои кредиты
        /// </summary>
        public decimal MonthlyCreditsPayment { get; set; }

        /// <summary>
        /// Количество взятых кредитов пользователем
        /// </summary>
        public byte CreditsCount { get; set; }

        /// <summary>
        /// Является ли пользователем данной платформы, или является пользователем другого банка
        /// </summary>
        public bool IsOnlineBankingUser { get; set; }

        /// <summary>
        /// Доход пользователя для предоставления кредита
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Подтверждён ли доход
        /// </summary>
        public bool IsIncomeVerified { get; set; }

        /// <summary>
        /// Адрес пользователя: улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Адрес пользователя: город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Адрес пользователя: индекс
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Аватар пользователя
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем один к одному
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
