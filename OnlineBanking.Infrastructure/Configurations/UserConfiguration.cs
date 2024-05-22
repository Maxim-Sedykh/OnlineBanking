using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
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
                    Username = "TestUser1",
                    Password = HashPasswordHelper.HashPassword("1234567"),
                    PassportCode = "541234",
                    Email = "test_mail@mail.ru",
                    Role = Role.User,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Username = "TestUser2",
                    Password = HashPasswordHelper.HashPassword("43214321"),
                    PassportCode = "612345",
                    Email = "testik_maike@bk.ru",
                    Role = Role.User,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.SenderTransactions)
                .WithOne(x => x.Sender)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.RecicipientTransactions)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
