using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Username = "Maximka",
                    Firstname = "Максим",
                    Surname = "Седых",
                    Password = HashPasswordHelper.HashPassword("1234567"),
                    Email = "max_se@bk.ru",
                    IsOnlineBankingUser = true,
                    Street = "Ленинская",
                    City = "Донецк",
                    ZipCode = "10011",
                    Income = 500000,
                    IsIncomeVerified = true,
                    CreditsCount = 1,
                    Avatar = null,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Username = "Vitaliy23",
                    Firstname = "Евгений",
                    Surname = "Морщинов",
                    Password = HashPasswordHelper.HashPassword("43214321"),
                    Email = "vit_de02_se@bk.ru",
                    IsOnlineBankingUser = true,
                    Street = "Плеханова",
                    City = "Макеевка",
                    ZipCode = "324232",
                    Income = 300000,
                    IsIncomeVerified = true,
                    CreditsCount = 1,
                    Avatar = null,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Firstname).HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.City).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Avatar).IsRequired(false);



            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.SenderTransactions)
                .WithOne(x => x.Sender)
                .HasForeignKey(x => x.SenderId);

            builder.HasMany(x => x.RecicipientTransactions)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);
        }
    }
}
