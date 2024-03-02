using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Card
{
    /// <summary>
    /// Модель представления для получения подробной информации по банковской карте
    /// </summary>
    public record CardViewModel
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime Validity { get; set; }

        public string CVV { get; set; }

        public long AccountId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
