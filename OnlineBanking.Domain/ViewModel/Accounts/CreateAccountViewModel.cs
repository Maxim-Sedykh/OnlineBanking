using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ViewModel.AccountType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Accounts
{
    /// <summary>
    /// Модель представления для создания нового счёта пользователя
    /// </summary>
    public record CreateAccountViewModel
    {
        [Required(ErrorMessage = "Укажите имя счёта")]
        [MinLength(4, ErrorMessage = "Длина имени счёта должна быть больше четырёх символов")]
        [MaxLength(50, ErrorMessage = "Длина имени счёта должна быть меньше 50 символов")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Укажите тип счёта")]
        public byte SelectedAccountTypeId { get; set; }

        public List<SelectAccountTypeViewModel> AccountTypes { get; set; }
    }
}
