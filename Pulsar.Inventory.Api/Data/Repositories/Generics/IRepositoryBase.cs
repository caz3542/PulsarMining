using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pulsar.Inventory.Api.Data.Repositories.Generics
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> FindAll();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<Guid> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}