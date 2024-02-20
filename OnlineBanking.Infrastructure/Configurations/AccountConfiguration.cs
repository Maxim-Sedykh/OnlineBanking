using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ValueObjects.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData(new List<Account>()
            {
                new Account
                {
                    Id = 1,
                    UserId = 1,
                    AccountType = AccountType.Savings,
                    Balance = new Money()
                    {
                        Amount = 20000,
                        Currency = Currency.RUB
                    },
                    CreatedAt = DateTime.UtcNow
                },
                new Account
                {
                    Id = 2,
                    UserId = 2,
                    AccountType = AccountType.Check,
                    Balance = new Money()
                    {
                        Amount = 100000,
                        Currency = Currency.USD
                    },
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.ComplexProperty(x => x.Balance);
        }
    }
}
