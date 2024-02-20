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
    internal class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.HasData(new List<Credit>()
            {
                new Credit
                {
                    Id = 1,
                    UserId = 1,
                    LoanSum = new Money()
                    {
                        Amount = 1000000,
                        Currency = Currency.RUB
                    },
                    Percent = 12.5f,
                    CreditTerm = DateTime.Now.AddYears(1),
                    LoanRemainder = new Money()
                    {
                        Amount = 500000,
                        Currency = Currency.RUB
                    },
                    CreatedAt = DateTime.UtcNow
                },
                new Credit
                {
                    Id = 2,
                    UserId = 2,
                    LoanSum = new Money()
                    {
                        Amount = 100000,
                        Currency = Currency.USD
                    },
                    Percent = 10.5f,
                    CreditTerm = DateTime.Now.AddYears(2),
                    LoanRemainder = new Money()
                    {
                        Amount = 50000,
                        Currency = Currency.USD
                    },
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.ComplexProperty(x => x.LoanRemainder);
            builder.ComplexProperty(x => x.LoanSum);
        }
    }
}
