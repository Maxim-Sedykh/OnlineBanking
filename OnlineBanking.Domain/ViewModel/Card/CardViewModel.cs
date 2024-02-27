﻿using OnlineBanking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.Card
{
    public class CardViewModel
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime Validity { get; set; }

        public string CVV { get; set; }

        public long AccountId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
