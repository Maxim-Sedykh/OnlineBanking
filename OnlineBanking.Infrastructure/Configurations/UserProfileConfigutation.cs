using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Emit;

namespace OnlineBanking.DAL.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasData(new List<UserProfile>()
            {
                new()
                {
                    Id = 1,
                    Firstname = "Максим",
                    Surname = "Седых",
                    MonthlyCreditsPayment = 9167,
                    CreditsCount = 1,
                    IsOnlineBankingUser = true,
                    Income = 500000,
                    IsIncomeVerified = true,
                    Street = "Ленинская",
                    City = "Донецк",
                    ZipCode = "10011",
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Firstname = "Евгений",
                    Surname = "Морщинов",
                    MonthlyCreditsPayment = 4167,
                    CreditsCount = 1,
                    IsOnlineBankingUser = true,
                    Income = 300000,
                    IsIncomeVerified = true,
                    Street = "Плеханова",
                    City = "Макеевка",
                    ZipCode = "324232",
                    UserId = 2,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Firstname).HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.City).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Avatar).IsRequired(false);

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserProfile)
                .HasPrincipalKey<UserProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
