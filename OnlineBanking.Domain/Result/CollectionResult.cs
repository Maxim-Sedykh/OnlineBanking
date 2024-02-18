using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Result
{
    public class CollectionResult<T> : Result<IEnumerable<T>>
    {
        public int Count { get; set; }
    }
}
