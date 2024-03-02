using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    /// <summary>
    /// Модель представления для получения подробной информации по кредиту
    /// </summary>
    public record CreditViewModel
    {
        public long Id { get; set; }

        public string CreditTypeName { get; set; }

        public decimal CreditSumAmount { get; set; }

        public decimal MoneyLenderReceiveAmount { get; set; }

        public decimal CreditRemainerAmount { get; set; }

        public decimal MonthlyPayment { get; set; }

        public float Percent { get; set; }

        public DateTime CreditTerm { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
