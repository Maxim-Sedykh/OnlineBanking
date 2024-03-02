using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.DAL.Context;
using OnlineBanking.DAL.Interceptors;
using OnlineBanking.DAL.Repositories;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.DependencyInjection
{
    /// <summary>
    /// Внедрение зависимостей слоя Data Access Layer
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString");

            services.AddSingleton<AuditInterceptor>();
            services.AddDbContext<BankingDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.InitRepositories();
        }

        private static void InitRepositories(this IServiceCollection services)
        {
            var types = new List<Type>()
            {
                typeof(Transaction),
                typeof(Credit),
                typeof(Account),
                typeof(PaymentMethod),
                typeof(Card),
                typeof(User),
                typeof(AccountType),
                typeof(CreditType),
                typeof(UserProfile),
            };

            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseRepository<>).MakeGenericType(type);
                var implementationType = typeof(BaseRepository<>).MakeGenericType(type);
                services.AddScoped(interfaceType, implementationType);
            }
        }
    }
}
