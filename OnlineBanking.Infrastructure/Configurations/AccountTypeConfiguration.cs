using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Configurations
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasData(new List<AccountType>()
            {
                new()
                {
                    Id = 1,
                    AccountTypeName = "Сберегательный",
                    Description = "Это типичный счет, предназначенный для накопления средств. Проценты начисляются на остаток на счете " +
                    "и могут быть выплачены ежемесячно, под данный тип счёта можно оформить дебетовую карту",
                    AnnualInterestRate = 20.5f,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 2,
                    AccountTypeName = "Текущий",
                    Description = "Текущий счет обычно используется для повседневных финансовых операций, " +
                    "и банки обычно не начисляют высокие проценты на остаток на таком счете, под данный тип счёта можно оформить дебетовую карту",
                    AnnualInterestRate = 2.5f,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 3,
                    AccountTypeName = "Кредитный",
                    Description = "Кредитный счет предоставляет возможность получить кредитные средства, и проценты начисляются на остаток " +
                    "задолженности по кредиту, проценты не отображаются в счёте, только в кредите, под данный тип счёта можно оформить кредитную карту",
                    AnnualInterestRate = 0,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.AccountTypeName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(e => e.AnnualInterestRate).IsRequired().HasDefaultValue(0f);

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.AccountType)
                .HasForeignKey(x => x.AccountTypeId);
        }
    }
}
