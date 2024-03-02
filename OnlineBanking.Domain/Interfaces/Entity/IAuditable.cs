using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Entity
{
    public interface IAuditable
    {
        /// <summary>
        /// Когда создан 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Когда в последний раз был обновлён
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }
    }
}
