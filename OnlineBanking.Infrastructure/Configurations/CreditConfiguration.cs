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
                new Credit
                {
                    Id = 1,
                    UserId = 1,
                    LoanSumAmount = 100000,
                    Percent = 12.5f,
                    CreditTerm = DateTime.UtcNow.AddYears(1),
                    LoanRemainerAmount = 50000,
                    CreatedAt = DateTime.UtcNow
                },
                new Credit
                {
                    Id = 2,
                    UserId = 2,
                    LoanSumAmount = 50000,
                    Percent = 10.5f,
                    CreditTerm = DateTime.UtcNow.AddYears(2),
                    LoanRemainerAmount = 25000,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
