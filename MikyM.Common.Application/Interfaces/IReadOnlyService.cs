using MikyM.Common.DataAccessLayer.Filters;
using MikyM.Common.DataAccessLayer.Specifications;
using MikyM.Common.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Interfaces
{
    public interface IReadOnlyService<TEntity, TResult> : IServiceBase where TEntity : AggregateRootEntity where TResult : class
    {
        Task<TResult> GetAsync(long id);
        Task<TGetResult> GetAsync<TGetResult>(long id) where TGetResult : AggregateRootEntity;
        Task<IReadOnlyList<TResult>> GetBySpecificationsAsync(ISpecifications<TEntity> specifications = null);
        Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity;
        Task<IReadOnlyList<TResult>> GetBySpecificationsAsync(PaginationFilterDto filter, ISpecifications<TEntity> specifications = null);
        Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(PaginationFilterDto filter, ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity;
        Task<long> CountAsync();
        Task<long> CountWhereAsync(ISpecifications<TEntity> specifications = null);
        Task<long> CountWhereAsync<TGetResult>(ISpecifications<TEntity> specifications = null) where TGetResult : AggregateRootEntity;
    }
}
