using Microsoft.EntityFrameworkCore.Storage;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Enum;
using OnlineBanking.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Interfaces.Database
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
