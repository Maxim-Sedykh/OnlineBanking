using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using OnlineBanking.DAL.Interceptors;
using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Context
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Passport> Passports { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
