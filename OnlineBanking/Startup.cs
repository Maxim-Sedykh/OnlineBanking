using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using OnlineBanking.Application.Jobs;
using Quartz;
using Quartz.Impl;
using System.Text;

namespace OnlineBanking
{
    public static class Startup
    {
        /// <summary>
        /// Подключение cookie авторизации и аутентификации
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthorization();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.Name = "OnlineBankingCookie";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static async Task AddMonthlyJob(this IServiceCollection services)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SetAccountPercentJob>()
                .WithIdentity("setAccountPercentJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("setAccountPercentJob", "group1")
                .StartNow()
                .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(dayOfMonth: 1, hour: 0, minute: 0))
                .Build();

            await scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);
        }
    }
}
