using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Application.Services;
using OnlineBanking.Application.Validators;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Interfaces.Validators.EntityValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Внедрение зависимостей слоя Application
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplication(this IServiceCollection services)
        {
            InitServices(services);
            InitValidators(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICreditService, CreditService>();
        }

        private static void InitValidators(this IServiceCollection services)
        {
            services.AddScoped<IAccountTypeValidator, AccountTypeValidator>();
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<ICardValidator, CardValidator>();
            services.AddScoped<ICreditValidator, CreditValidator>();
            services.AddScoped<ICreditTypeValidator, CreditTypeValidator>();
            services.AddScoped<IPaymentMethodValidator, PaymentMethodValidator>();
            services.AddScoped<ITransactionValidator, TransactionValidator>();
            services.AddScoped<IUserValidator, UserValidator>();
        }
    }
}
