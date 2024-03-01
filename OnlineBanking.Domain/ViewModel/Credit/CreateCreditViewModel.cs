﻿using OnlineBanking.Domain.ViewModel.CreditType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    public class CreateCreditViewModel
    {
        public decimal MoneyLenderReceiveAmount { get; set; }

        public List<SelectCreditTypeViewModel> CreditTypes { get; set; }

        public byte SelectedCreditTypeId { get; set; }

        public DateTime CreditTerm { get; set; }
    }
}
