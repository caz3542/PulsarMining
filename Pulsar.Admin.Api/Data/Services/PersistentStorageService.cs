using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Pulsar.Admin.Api.Data.BaseModels;
using Pulsar.Admin.Api.Data.Repositories.Generics;
using Pulsar.Admin.Api.Infrastructure.FluxTimeServerClientServices;
using Pulsar.Admin.Api.Infrastructure.HealthServices;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace Pulsar.Admin.Api.Data.Services
{
    public class PersistentStorageService<TViewModel, TDatabaseModel> : IPersistentStorageService<TViewModel, TDatabaseModel> where TViewModel : class where TDatabaseModel : BaseDbEntity
    {
        private readonly ILogger<PersistentStorageService<TViewModel, TDatabaseModel>> _logger;
        private readonly IRepositoryBase<TDatabaseModel> _repositoryBase;
        private readonly IMapper _mapper;
        private readonly HealthService _healthService;
        private readonly FluxTimeServerClientService _fluxTimeServerClient;

        public PersistentStorageService(ILogger<PersistentStorageService<TViewModel, TDatabaseModel>> logger,
            IRepositoryBase<TDatabaseModel> repositoryBase,
            IMapper mapper,
            HealthService healthService, FluxTimeServerClientService fluxTimeServerClient)
        {
            _logger = logger;
            _repositoryBase = repositoryBase;
            _mapper = mapper;
            _healthService = healthService;
            _fluxTimeServerClient = fluxTimeServerClient;
        }

        public async Task<List<TViewModel>> GetAllAsync()
        {
            try
            {
                var result = await _repositoryBase.FindAll();
                var mappedResult = _mapper.Map<List<TDatabaseModel>, List<TViewModel>>(result);

                return mappedResult;

            }
            catch (Exception e)
            {
                _logger.LogCritical($"STORAGE: Exception occured {e.Message}");
                _healthService.SetCritical("STORAGE: Unable to retrieve data from persistent storage");
                throw;
            }
        }

        public async Task<List<TViewModel>> GetByExpressionAsync(Expression<Func<TDatabaseModel, bool>> expression)
        {
            try
            {
                var result = await _repositoryBase.FindByCondition(expression);
                var mappedResult = _mapper.Map<List<TDatabaseModel>, List<TViewModel>>(result);
                return mappedResult;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"STORAGE: Exception occured {e.Message}");
                _healthService.SetCritical("STORAGE: Unable to retrieve data from persistent storage");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(TViewModel entity)
        {
            try
            {
                var mappedEntity = _mapper.Map<TViewModel, TDatabaseModel>(entity);
                mappedEntity.Id = new Guid();
                var result = await _repositoryBase.Create(mappedEntity);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"STORAGE: Exception occured {e.Message}");
                _healthService.SetCritical("STORAGE: Unable to store to persistent storage");
                throw;
            }
        }

        public async Task UpdateAsync(TViewModel entity)
        {
            try
            {
                var mappedEntity = _mapper.Map<TViewModel, TDatabaseModel>(entity);
                await _repositoryBase.Update(mappedEntity);

            }
            catch (Exception e)
            {
                _logger.LogCritical($"STORAGE: Exception occured {e.Message}");
                _healthService.SetCritical("STORAGE: Unable to update to persistent storage");
                throw;
            }
        }

        public async Task DeleteAsync(TViewModel entity)
        {
            try
            {
                var mappedEntity = _mapper.Map<TViewModel, TDatabaseModel>(entity);
                await _repositoryBase.Delete(mappedEntity);

            }
            catch (Exception e)
            {
                _logger.LogCritical($"STORAGE: Exception occured {e.Message}");
                _healthService.SetCritical("STORAGE: Unable to delete to persistent storage");
                throw;
            }
        }
    }
}