using MikyM.Common.DataAccessLayer.Filters;
using MikyM.Common.DataAccessLayer.Specifications;
using MikyM.Common.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Interfaces
{
    public interface IReadOnlyService<TEntity> : IServiceBase where TEntity : AggregateRootEntity
    {
        Task<TGetResult> GetAsync<TGetResult>(long id) where TGetResult : class;

        Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(
            ISpecifications<TEntity> specifications = null) where TGetResult : class;

        Task<IReadOnlyList<TGetResult>> GetBySpecificationsAsync<TGetResult>(PaginationFilterDto filter,
            ISpecifications<TEntity> specifications = null) where TGetResult : class;

        Task<long> CountAsync();
        Task<long> CountWhereAsync(ISpecifications<TEntity> specifications = null);
        Task<long> CountWhereAsync<TGetResult>(ISpecifications<TEntity> specifications = null) where TGetResult : class;
    }
}