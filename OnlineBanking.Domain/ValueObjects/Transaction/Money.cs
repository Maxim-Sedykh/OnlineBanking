using OnlineBanking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.ValueObjects.Transaction
{
    [ComplexType]
    public class Money
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
    }
}
