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
    internal class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.HasData(new List<Credit>()
            {
                new()
                {
                    Id = 1,
                    UserId = 1,
                    CreditSumAmount = 110000,
                    MoneyLenderReceiveAmount = 100000,
                    CreditTerm = DateTime.UtcNow.AddYears(1),
                    CreditRemainerAmount = 50000,
                    CreatedAt = DateTime.UtcNow,
                    MonthlyPayment = 9167,
                    CreditTypeId = 1,
                },
                new()
                {
                    Id = 2,
                    UserId = 2,
                    CreditSumAmount = 50000,
                    CreditTerm = DateTime.UtcNow.AddYears(12),
                    CreditRemainerAmount = 25000,
                    CreatedAt = DateTime.UtcNow,
                    MonthlyPayment = 4167,
                    CreditTypeId = 1,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
