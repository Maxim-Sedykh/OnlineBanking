using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class User:IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Код паспорта
        /// </summary>
        public string PassportCode { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя (Обычный пользователь, Админ)
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Все счета пользователя
        /// </summary>
        public List<Account> Accounts { get; set; }

        /// <summary>
        /// Все транзакции пользователя в качестве отправителя
        /// </summary>
        public List<Transaction> SenderTransactions { get; set; }

        /// <summary>
        /// Все транзакции пользователя в качестве получателя
        /// </summary>
        public List<Transaction> RecicipientTransactions { get; set; }

        /// <summary>
        /// Все кредиты пользователя
        /// </summary>
        public List<Credit> Credits { get; set; }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public UserProfile UserProfile { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
