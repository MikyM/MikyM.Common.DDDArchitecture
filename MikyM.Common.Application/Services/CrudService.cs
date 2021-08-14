using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikyM.Common.Application.Interfaces;
using MikyM.Common.DataAccessLayer.Repositories;
using MikyM.Common.DataAccessLayer.UnitOfWork;
using MikyM.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Services
{
    public class CrudService<TEntity, T, TContext> : ReadOnlyService<TEntity, T, TContext>, ICrudService<TEntity, T> where TEntity : AggregateRootEntity where T : class where TContext : DbContext
    {
        protected CrudService(IMapper mapper, IUnitOfWork<TContext> uof) : base(mapper, uof)
        {
        }

        public async Task<long> AddAsync(T addObject, bool shouldSave = false)
        {
            var entity = _mapper.Map<TEntity>(addObject);
            try
            {
                await _unitOfWork.GetRepository<Repository<TEntity>>().AddAsync(entity);

                if (shouldSave)
                    await CommitAsync();
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }

            return entity.Id;
        }

        public async Task<long> AddAsync<TPost>(TPost addObject, bool shouldSave = false) where TPost : AggregateRootEntity
        {
            var entity = _mapper.Map<TEntity>(addObject);
            await _unitOfWork.GetRepository<Repository<TEntity>>().AddAsync(entity);
            if (shouldSave)
                await CommitAsync();
            else
                return 0;
            return entity.Id;
        }

        public async Task<bool> UpdateAsync<TPatch>(TPatch updateObject, bool shouldSave = false) where TPatch : AggregateRootEntity
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Update(_mapper.Map<TEntity>(updateObject));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }
        

        public async Task<bool> UpdateAsync(T updateObject, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Update(_mapper.Map<TEntity>(updateObject));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> updateObjects, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().UpdateRange(_mapper.Map<IEnumerable<TEntity>>(updateObjects));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }

        public async Task<bool> UpdateRangeAsync<TPatch>(IEnumerable<TPatch> updateObjects, bool shouldSave = false) where TPatch : AggregateRootEntity
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().UpdateRange(_mapper.Map<IEnumerable<TEntity>>(updateObjects));
            if (shouldSave)
                await CommitAsync();
            return true;
            //to do
        }

        public async Task<long> AddOrUpdateAsync(T putObject, bool shouldSave = false)
        {
            var entity = _mapper.Map<TEntity>(putObject);
            _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdate(entity);
            if (shouldSave)
                await CommitAsync();
            else
                return 0;
            return entity.Id;
        }

        public async Task<long> AddOrUpdateAsync<TPut>(TPut putObject, bool shouldSave = false) where TPut : AggregateRootEntity
        {
            var entity = _mapper.Map<TEntity>(putObject);
            _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdate(entity);
            if (shouldSave)
                await CommitAsync();
            else
                return 0;
            return entity.Id;
        }

        public async Task<List<long>> AddOrUpdateRangeAsync(IEnumerable<T> putObjects, bool shouldSave = false)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(putObjects).ToList();
                _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdateRange(entities);
            if (shouldSave)
                await CommitAsync();
            else
                return new List<long>();
            return entities.Select(x => x.Id).ToList();
        }

        public async Task<List<long>> AddOrUpdateRangeAsync<TPut>(IEnumerable<TPut> putObjects, bool shouldSave = false) where TPut : AggregateRootEntity
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(putObjects).ToList();
            _unitOfWork.GetRepository<Repository<TEntity>>().AddOrUpdateRange(entities);

            if (shouldSave)
                await CommitAsync();
            else
                return new List<long>();
            return entities.Select(x => x.Id).ToList();
        }

        public async Task<bool> DeleteAsync(T deleteObject, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Delete(_mapper.Map<TEntity>(deleteObject));

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public async Task<bool> DeleteAsync<TDelete>(TDelete deleteObject, bool shouldSave = false) where TDelete : AggregateRootEntity
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Delete(_mapper.Map<TEntity>(deleteObject));

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public async Task<bool> DeleteAsync(long id, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().Delete(id);
            if (shouldSave)
                await CommitAsync();
            else
                return false;
            return true;
            //to do
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<long> deleteObjects, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>().DeleteRange(deleteObjects);

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }

        public async Task<bool> DeleteRangeAsync<TDelete>(IEnumerable<TDelete> deleteObjects, bool shouldSave = false) where TDelete : AggregateRootEntity
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

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> dtos, bool shouldSave = false)
        {
            _unitOfWork.GetRepository<Repository<TEntity>>()
                .DeleteRange(_mapper.Map<IEnumerable<TEntity>>(dtos));

            if (shouldSave)
                await CommitAsync();
            else
                return true;
            return true;
            //to do
        }
    }
}
