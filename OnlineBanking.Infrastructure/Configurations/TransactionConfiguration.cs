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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasData(new List<Transaction>() 
            {
                new Transaction()
                {
                    Id = 1,
                    SenderId = 1,
                    RecipientId = 2,
                    TransactionDate = DateTime.UtcNow.AddDays(3),
                    MoneyAmount = 500,
                    CreatedAt = DateTime.UtcNow,
                    PaymentMethodId = 3
                },
                new Transaction()
                {
                    Id = 2,
                    SenderId = 2,
                    RecipientId = 1,
                    TransactionDate = DateTime.UtcNow.AddDays(4),
                    MoneyAmount = 10000,
                    CreatedAt = DateTime.UtcNow,
                    PaymentMethodId = 1
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
