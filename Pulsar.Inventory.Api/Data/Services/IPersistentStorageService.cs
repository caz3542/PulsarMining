using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pulsar.Inventory.Api.Data.Services
{
    /// <summary>
    /// Generic interface to CRUD persistent storage. Responsible for mapping between DB and ViewModels
    /// </summary>
    /// <typeparam name="TViewModel">View Model Type</typeparam>
    /// <typeparam name="TDatabaseModel">Database Model Type</typeparam>
    public interface IPersistentStorageService<TViewModel, TDatabaseModel>
    {
        Task<List<TViewModel>> GetAllAsync();
        Task<List<TViewModel>> GetByExpressionAsync(Expression<Func<TDatabaseModel, bool>> expression);
        Task<Guid> CreateAsync(TViewModel entity);
        Task UpdateAsync(TViewModel entity);
        Task DeleteAsync(TViewModel entity);
    }
}