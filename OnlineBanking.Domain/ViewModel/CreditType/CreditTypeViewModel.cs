using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ViewModel.CreditType
{
    public class CreditTypeViewModel
    {
        public byte Id { get; set; }

        public string CreditTypeName { get; set; }

        public string Description { get; set; }

        public short MinCreaditTermInMonths { get; set; }

        public short MaxCreaditTermInMonths { get; set; }

        public float InterestRate { get; set; }
    }
}
