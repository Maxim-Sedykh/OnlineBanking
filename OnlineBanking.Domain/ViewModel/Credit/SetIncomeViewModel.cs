﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Credit
{
    public class SetIncomeViewModel
    {
        [Required]
        public decimal UserIncome { get; set; }
    }
}
