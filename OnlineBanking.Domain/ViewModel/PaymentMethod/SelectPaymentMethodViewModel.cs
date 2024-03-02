using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.PaymentMethod
{
    /// <summary>
    /// Модель представления для поля для выбора метода платежа при совершении транзакции
    /// </summary>
    public record SelectPaymentMethodViewModel
    {
        public byte Id { get; set; }

        public string Name { get; set; }
    }
}
