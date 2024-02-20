using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using OnlineBanking.Domain.ValueObjects.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class User:IEntityId<int>, IAuditable
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsOnlineBankingUser { get; set; }

        public Role Role { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public List<Account> Accounts { get; set; }

        public List<Transaction> SenderTransactions { get; set; }

        public List<Transaction> RecicipientTransactions { get; set; }

        public List<Credit> Credits { get; set; }

        public UserToken UserToken { get; set; }
    }
}
