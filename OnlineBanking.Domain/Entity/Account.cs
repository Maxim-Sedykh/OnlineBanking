﻿using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entity
{
    public class Account : IEntityId<int>, IAuditable
    {
        public int Id { get; set; }
        
        public string AccountName { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public AccountType AccountType { get; set; }

        public decimal BalanceAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
