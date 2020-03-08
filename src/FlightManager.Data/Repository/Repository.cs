using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManager.Data.Repository
{
    using System.Threading.Tasks;
    using FlightManager.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly FlightManagerDbContext context;

        private readonly DbSet<TEntity> set;

        public Repository(FlightManagerDbContext context)
        {
            this.context = context;
            this.set = this.context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
            => this.set;

        public Task AddAsync(TEntity entity)
            => this.set.AddAsync(entity).AsTask();
        
        public void Remove(TEntity entity)
            => this.set.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entity)
            => this.set.RemoveRange(entity);

        public void Update(TEntity entity)
            => this.set.Update(entity);

        public Task<int> SaveChangesAsync()
            => this.context.SaveChangesAsync();

        public void Dispose()
            => this.context.Dispose();
    }
}