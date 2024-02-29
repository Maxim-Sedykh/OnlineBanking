using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Configurations
{
    public class CreditTypeConfiguration : IEntityTypeConfiguration<CreditType>
    {
        public void Configure(EntityTypeBuilder<CreditType> builder)
        {
            builder.HasData(new List<CreditType>()
            {
                new()
                {
                    Id = 1,
                    CreditTypeName = "Потребительский",
                    Description = "Предназначен для покупки товаров, услуг, оплаты отпуска и других личных потребностей",
                    MinCreaditTermInMonths = 6,
                    MaxCreaditTermInMonths = 60,
                    InterestRate = 15,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 2,
                    CreditTypeName = "Ипотечный",
                    Description = "Предоставляется для покупки или строительства жилой недвижимости",
                    MinCreaditTermInMonths = 60,
                    MaxCreaditTermInMonths = 360,
                    InterestRate = 5,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 3,
                    CreditTypeName = "Автокредит",
                    Description = "Используется для покупки автомобиля",
                    MinCreaditTermInMonths = 12,
                    MaxCreaditTermInMonths = 84,
                    InterestRate = 10,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = 4,
                    CreditTypeName = "Бизнес-кредит",
                    Description = "Выдается компаниям для развития бизнеса",
                    MinCreaditTermInMonths = 6,
                    MaxCreaditTermInMonths = 60,
                    InterestRate = 15,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreditTypeName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);

            builder.HasMany(x => x.Credits)
                .WithOne(x => x.CreditType)
                .HasForeignKey(x => x.Cred);
        }
    }
}
