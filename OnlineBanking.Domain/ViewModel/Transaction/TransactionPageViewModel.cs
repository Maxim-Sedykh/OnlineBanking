using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Transaction
{
    /// <summary>
    /// Модель представления для получения информации по транзакциям пользователя
    /// </summary>
    public record TransactionPageViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; }
    }
}
