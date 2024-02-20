using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ValueObjects.Transaction;
using OnlineBanking.Domain.ValueObjects.User;
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
                    Money = new Money()
                    {
                        Amount = 520.33m,
                        Currency = Currency.USD,
                    },
                    CreatedAt = DateTime.UtcNow,
                },
                new Transaction()
                {
                    Id = 2,
                    SenderId = 2,
                    RecipientId = 1,
                    TransactionDate = DateTime.UtcNow.AddDays(4),
                    Money = new Money()
                    {
                        Amount = 600m,
                        Currency = Currency.RUB,
                    },
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.ComplexProperty(x => x.Money);
        }
    }
}
