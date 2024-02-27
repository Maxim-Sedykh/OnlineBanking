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
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasData(new List<Card>()
            {
                new()
                {
                    Id = 1,
                    CardNumber = "2003536478934176",
                    Validity = DateTime.UtcNow.AddYears(7),
                    CVV = "356",
                    AccountId = 1,
                    CreatedAt = DateTime.UtcNow
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.CVV).IsRequired().HasMaxLength(4);
        }
    }
}
