using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
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
                    BalanceAmount = 10000m,
                    CreatedAt = DateTime.UtcNow
                },
                new Account
                {
                    Id = 2,
                    UserId = 2,
                    AccountType = AccountType.Check,
                    BalanceAmount = 20000m,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.AccountName).IsRequired().HasMaxLength(50);
        }
    }
}
