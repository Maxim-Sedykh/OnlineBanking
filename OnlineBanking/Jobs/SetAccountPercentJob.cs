using OnlineBanking.Application.Services;
using OnlineBanking.Controllers;
using OnlineBanking.Domain.Interfaces.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Application.Jobs
{
    public class SetAccountPercentJob : IJob
    {
        private readonly IAccountService _accountService;

        public SetAccountPercentJob(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var controller = new AccountController(_accountService);
            await controller.SetAccountPercent();
        }
    }
}
