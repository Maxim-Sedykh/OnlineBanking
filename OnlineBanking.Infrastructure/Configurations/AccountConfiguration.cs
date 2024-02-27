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
                new()
                {
                    Id = 1,
                    AccountName = "Сберегательный счёт для кредита",
                    UserId = 1,
                    AccountTypeId = 1,
                    BalanceAmount = 10000m,
                    IsCardLinked = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 2,
                    AccountName = "Сберегательный счёт",
                    UserId = 2,
                    IsCardLinked = false,
                    AccountTypeId = 2,
                    BalanceAmount = 20000m,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.AccountName).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Card)
                    .WithOne(x => x.Account)
                    .HasPrincipalKey<Account>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
