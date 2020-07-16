using Pulsar.CoreElements.Api.Data.BaseModels;
using Pulsar.CoreElements.Api.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pulsar.CoreElements.Api.Data.Repositories.Generics
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseDbEntity
    {
        private DatabaseContext RepositoryContext { get; set; }

        public RepositoryBase(DatabaseContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<List<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>()
                .Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<Guid> Create(T entity)
        {
            entity.Id = new Guid();
            await this.RepositoryContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        private async Task<int> SaveChangesAsync()
        {
            return await this.RepositoryContext.SaveChangesAsync();
        }
    }
}