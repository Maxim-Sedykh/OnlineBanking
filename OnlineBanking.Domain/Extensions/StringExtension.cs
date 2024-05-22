using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Extensions
{
    public static class StringExtension
    {
        public static string JoinErrors(this List<string> words)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < words.Count; i++)
            {
                sb.Append($"{i + 1}: {words[i]} ");
            }

            return sb.ToString();
        }
    }
}
