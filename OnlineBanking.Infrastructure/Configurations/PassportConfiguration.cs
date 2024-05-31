using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;

namespace OnlineBanking.DAL.Configurations
{
    public class PassportConfiguration : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new List<Passport>()
            {
                new Passport()
                {
                    Id = 1,
                    PassportId = "1234",
                    PassportSeries = "654321",
                    DateOfIssue = DateTime.Now,
                    IsConfirmed = true,
                    IssuedBy = "ГУ МВД России по Ростовской области",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                },
                new Passport()
                {
                    Id = 2,
                    PassportId = "4321",
                    PassportSeries = "123456",
                    DateOfIssue = DateTime.Now,
                    IsConfirmed = true,
                    IssuedBy = "ГУ МВД России по Ростовской области",
                    UserId = 2,
                    CreatedAt = DateTime.Now,
                }
            });

            builder.Property(x => x.PassportSeries).IsRequired().HasMaxLength(10);
            builder.Property(x => x.PassportId).IsRequired().HasMaxLength(10);
            builder.Property(x => x.IssuedBy).IsRequired().HasMaxLength(200);
        }
    }
}
