using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikyM.Common.Application.Interfaces;
using MikyM.Common.DataAccessLayer.Repositories;
using MikyM.Common.DataAccessLayer.UnitOfWork;
using MikyM.Common.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Services
{
    public class CrudService<TEntity, TContext> : ReadOnlyService<TEntity, TContext>, ICrudService<TEntity> where TEntity : AggregateRootEntity where TContext : DbContext
    {
        public CrudService(IMapper mapper, IUnitOfWork<TContext> uof) : base(mapper, uof)
        {
        }

        public virtual async Task<long> AddAsync<TPost>(TPost addObject, bool shouldSave = false) where TPost : class
        {
            var entity = _mapper.Map<TEntity>(addObject);
            await _unitOfWork.GetRepository<Repository<TEntity>>().AddAsync(entity);
            if (shouldSave)
                await CommitAsync();
            else
                return 0;
            return entity.Id;
        }

        public virtual async Task<bool> UpdateAsync<TPatch>(TPatch updateObject, bool shouldSave = false) where TPatch : class
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Update(_mapper.Map<TEntity>(updateObject));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }

        public virtual async Task<bool> UpdateRangeAsync<TPatch>(IEnumerable<TPatch> updateObjects, bool shouldSave = false) where TPatch : class
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().UpdateRange(_mapper.Map<IEnumerable<TEntity>>(updateObjects));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }

        public virtual async Task<long> AddOrUpdateAsync<TPut>(TPut putObject, bool shouldSave = false) where TPut : class
        {
            var entity = _mapper.Map<TEntity>(putObject);
            _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdate(entity);
            if (shouldSave)
                await CommitAsync();
            else
                return 0;
            return entity.Id;
        }

        public virtual async Task<List<long>> AddOrUpdateRangeAsync<TPut>(IEnumerable<TPut> putObjects, bool shouldSave = false) where TPut : class
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(putObjects).ToList();
            _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdateRange(entities);

            if (shouldSave)
                await CommitAsync();
            else
                return new List<long>();
            return entities.Select(x => x.Id).ToList();
        }

        public virtual async Task<bool> DeleteAsync<TDelete>(TDelete deleteObject, bool shouldSave = false) where TDelete : class
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Delete(_mapper.Map<TEntity>(deleteObject));

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public virtual async Task<bool> DeleteAsync(long id, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Delete(id);
            if (shouldSave)
                await CommitAsync();
            else
                return false;
            return true;
            //to do
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<long> deleteObjects, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().DeleteRange(deleteObjects);

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public virtual async Task<bool> DeleteRangeAsync<TDelete>(IEnumerable<TDelete> deleteObjects, bool shouldSave = false) where TDelete : class
        {
            _unitOfWork.GetRepository<Repository<TEntity>>()
                .DeleteRange(_mapper.Map<IEnumerable<TEntity>>(deleteObjects));

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public virtual async Task<bool> DisableAsync(long id, bool shouldSave = false)
        {
            await _unitOfWork.GetRepository<Repository<TEntity>>()
                .DisableAsync(id);
            if (shouldSave)
                await CommitAsync();
            else return true;
            return true;
        }

        public virtual async Task<bool> DisableAsync<TDisable>(TDisable objToDisable, bool shouldSave = false) where TDisable : class
        {
            _unitOfWork.GetRepository<Repository<TEntity>>()
                .Disable(_mapper.Map<TEntity>(objToDisable));
            if (shouldSave)
                await CommitAsync();
            else return true;
            return true;
        }
        public virtual async Task<bool> DisableRangeAsync(IEnumerable<long> ids, bool shouldSave = false)
        {
            await _unitOfWork.GetRepository<Repository<TEntity>>()
                .DisableRangeAsync(ids);
            if (shouldSave)
                await CommitAsync();
            else return true;
            return true;
        }

        public virtual async Task<bool> DisableRangeAsync<TDisable>(IEnumerable<TDisable> objsToDisable, bool shouldSave = false) where TDisable : class
        {
            var aggregateRootEntities = _mapper.Map<IEnumerable<TEntity>>(objsToDisable);
            _unitOfWork.GetRepository<Repository<TEntity>>()
                .DisableRange(aggregateRootEntities);
            if (shouldSave)
                await CommitAsync();
            else return true;
            return true;
        }
    }
}
