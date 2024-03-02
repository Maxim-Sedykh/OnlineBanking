using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Card
{
    /// <summary>
    /// Модель представления для получения информации о банковской карте
    /// </summary>
    public record CardActionViewModel
    {
        public int id { get; set; }
    }
}
