using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Error
{
    /// <summary>
    //  Модель представления предназначенная для предоставления информации об ошибке
    /// </summary>
    public record ErrorViewModel
    {
        public string ErrorMessage { get; set; }
    }
}
