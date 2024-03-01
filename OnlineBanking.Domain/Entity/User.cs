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

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public decimal MonthlyCreditsPayment { get; set; }

        public byte CreditsCount { get; set; }

        public string Password { get; set; }

        public bool IsOnlineBankingUser { get; set; }

        public Role Role { get; set; }

        public decimal Income { get; set; }

        public bool IsIncomeVerified { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public byte[] Avatar { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public List<Account> Accounts { get; set; }

        public List<Transaction> SenderTransactions { get; set; }

        public List<Transaction> RecicipientTransactions { get; set; }

        public List<Credit> Credits { get; set; }
    }
}
