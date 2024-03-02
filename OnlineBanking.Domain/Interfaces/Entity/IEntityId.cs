using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Entity
{
    public interface IEntityId<T> where T: struct
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public T Id { get; set; }
    }
}
