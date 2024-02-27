using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Helpers
{

    public static class BankCardDataGenerator
    {
        private const byte CardNumberDigitCount = 16;
        private const byte CVVDigitCount = 3;
        private const string BankCardIdentifier = "200353";
        private static Random random = new Random();

        public static string GenerateCardNumber()
        {
            StringBuilder cardNumberBuilder = new StringBuilder(BankCardIdentifier, CardNumberDigitCount);

            for (int i = 0; i < CardNumberDigitCount - BankCardIdentifier.Length; i++) // Учитываем длину начального значения
            {
                cardNumberBuilder.Append(random.Next(0, 10));
            }

            return cardNumberBuilder.ToString();
        }

        public static string GenerateCVV()
        {
            StringBuilder cvvBuilder = new StringBuilder(CVVDigitCount);

            for (int i = 0; i < CVVDigitCount; i++)
            {
                cvvBuilder.Append(random.Next(0, 10));
            }

            return cvvBuilder.ToString();
        }
    }
   
}
