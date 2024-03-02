using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.AccountType
{
    /// <summary>
    /// Модель представления для получения подробной информации по типам счёта
    /// </summary>
    public record AccountTypeViewModel
    {
        public byte Id { get; set; }

        public string AccountTypeName { get; set; }

        public string Description { get; set; }

        public float AnnualInterestRate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
