using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikyM.Common.Application.Interfaces;
using MikyM.Common.DataAccessLayer.Filters;
using MikyM.Common.DataAccessLayer.Repositories;
using MikyM.Common.DataAccessLayer.Specifications;
using MikyM.Common.DataAccessLayer.UnitOfWork;
using MikyM.Common.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Services
{
    public class ReadOnlyService<TEntity, TResult, TContext> : ServiceBase<TContext>, IReadOnlyService<TEntity, TResult> where TEntity : AggregateRootEntity where TResult : class where TContext : DbContext
    {
        public ReadOnlyService(IMapper mapper, IUnitOfWork<TContext> uof) : base(mapper, uof)
        {
        }

        public async Task<TResult> GetAsync(long id)
        {
            return _mapper.Map<TResult>(await _unitOfWork.GetRepository<TEntity, ReadOnlyRepository<TEntity>>().GetAsync(id));
        }

        public async Task<TGetResult> GetAsync<TGetResult>(long id) where TGetResult : AggregateRootEntity
        {
            return _mapper.Map<TGetResult>(await _unitOfWork.GetRepository<TEntity, ReadOnlyRepository<TEntity>>().GetAsync(id));
        }

        public async Task<IReadOnlyList<TResult>> GetBySpecificationsAsync(ISpecifications<TEntity> specifications = null)
        {
            return _mapper.Map<IReadOnlyList<TResult>>(
                await _unitOfWork
                    .GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                    .GetBySpecificationsAsync(specifications));
        }

        public async Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity
        {
            return _mapper.Map<IReadOnlyList<TGetResult>>(
                await _unitOfWork
                    .GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                    .GetBySpecificationsAsync(specifications));
        }

        public async Task<IReadOnlyList<TResult>> GetBySpecificationsAsync(PaginationFilterDto filter, ISpecifications<TEntity> specifications = null)
        {
            return _mapper.Map<IReadOnlyList<TResult>>(
                await _unitOfWork
                    .GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                    .GetBySpecificationsAsync(_mapper.Map<PaginationFilter>(filter), specifications));
        }

        public async Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(PaginationFilterDto filter, ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity
        {
            return _mapper.Map<IReadOnlyList<TGetResult>>(
                await _unitOfWork
                    .GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                    .GetBySpecificationsAsync(_mapper.Map<PaginationFilter>(filter), specifications));
        }

        public async Task<long> CountAsync()
        {
            return await _unitOfWork.GetRepository<TEntity, ReadOnlyRepository<TEntity>>().CountAsync();
        }

        public async Task<long> CountWhereAsync(ISpecifications<TEntity> specifications = null)
        {
            return await _unitOfWork.GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                .CountWhereAsync(specifications);
        }

        public async Task<long> CountWhereAsync<TGetResult>(ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity
        {
            return await _unitOfWork.GetRepository<TEntity, ReadOnlyRepository<TEntity>>()
                .CountWhereAsync(specifications);
        }
    }
}
