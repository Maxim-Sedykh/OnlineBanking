using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.ValueObjects.User;
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
                new User
                {
                    Id = 1,
                    Username = "Maximka",
                    Firstname = "Максим",
                    Surname = "Седых",
                    Password = "1234567",
                    Email = "max_se@bk.ru",
                    IsOnlineBankingUser = true,
                    Address = new Address()
                    {
                        Street = "Октябрьская",
                        City = "Макеевка",
                        ZipCode = "43215"
                    },
                    CreatedAt = DateTime.UtcNow,
                },
                new User
                {
                    Id = 2,
                    Username = "Vitaliy23",
                    Firstname = "Евгений",
                    Surname = "Морщинов",
                    Password = "43214321",
                    Email = "vit_de02_se@bk.ru",
                    IsOnlineBankingUser = true,
                    Address = new Address()
                    {
                        Street = "Володинская",
                        City = "Донецк",
                        ZipCode = "11001"
                    },
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Firstname).HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired();

            builder.ComplexProperty(x => x.Address);
            builder.Property(x => x.Address.Street).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address.City).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address.ZipCode).IsRequired().HasMaxLength(10);

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.SenderTransactions)
                .WithOne(x => x.Sender)
                .HasForeignKey(x => x.SenderId);

            builder.HasMany(x => x.RecicipientTransactions)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);

            builder.HasMany(x => x.Credits)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
