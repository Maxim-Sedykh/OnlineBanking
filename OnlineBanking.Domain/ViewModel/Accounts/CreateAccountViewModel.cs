using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.ViewModel.AccountType;
using System;
using System.Collections.Generic;
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
        public string AccountName { get; set; }

        public byte SelectedAccountTypeId { get; set; }

        public List<SelectAccountTypeViewModel> AccountTypes { get; set; }
    }
}
