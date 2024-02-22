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
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasData(new List<PaymentMethod>()
            {
                new PaymentMethod
                {
                    Id = 1,
                    Name = "Карта Visa",
                    Description = "Банковская карта Visa",
                    CreatedAt = DateTime.UtcNow,
                },
                new PaymentMethod
                {
                    Id = 2,
                    Name = "Online-сервис VkPay",
                    Description = "Онлайн сервис в мессенджере VK - VkPay",
                    CreatedAt = DateTime.UtcNow,
                },
                new PaymentMethod
                {
                    Id = 3,
                    Name = "Интернет-кошелёк Qiwi",
                    Description = "Интернет-кошелёк Qiwi",
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(200);

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey(x => x.PaymentMethodId);
        }
    }
}
