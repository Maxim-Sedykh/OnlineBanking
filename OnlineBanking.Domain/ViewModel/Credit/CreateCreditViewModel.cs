using OnlineBanking.Domain.ViewModel.CreditType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    /// <summary>
    /// Модель представления для создания кредита
    /// </summary>
    public record CreateCreditViewModel
    {
        [Required(ErrorMessage = "Выберите количество денег кредита")]
        public decimal MoneyLenderReceiveAmount { get; set; }

        public List<SelectCreditTypeViewModel> CreditTypes { get; set; }

        public byte SelectedCreditTypeId { get; set; }

        [Required(ErrorMessage = "Выберите срок кредита")]
        public DateTime CreditTerm { get; set; }
    }
}
