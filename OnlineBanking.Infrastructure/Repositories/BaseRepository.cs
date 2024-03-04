using Microsoft.EntityFrameworkCore;
using OnlineBanking.DAL.Context;
using OnlineBanking.Domain.Entity;
using OnlineBanking.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BankingDbContext _dbContext;

        public BaseRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            ValidateEntityOnNull(entity);

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Entities is null");
            }
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        private void ValidateEntityOnNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity is null");
            }
        }
    }
}
