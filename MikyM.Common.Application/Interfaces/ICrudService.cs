using System.Collections.Generic;
using System.Threading.Tasks;
using MikyM.Common.Domain.Entities;

namespace MikyM.Common.Application.Interfaces
{
    public interface ICrudService<TEntity> : IReadOnlyService<TEntity> where TEntity : AggregateRootEntity
    {
        Task<long> AddAsync<TPost>(TPost addObject, bool shouldSave = false) where TPost : class;
        Task<bool> UpdateAsync<TPatch>(TPatch patchObject, bool shouldSave = false) where TPatch : class;

        Task<bool> UpdateRangeAsync<TPatch>(IEnumerable<TPatch> patchObjects, bool shouldSave = false)
            where TPatch : class;

        Task<long> AddOrUpdateAsync<TPut>(TPut putObject, bool shouldSave = false) where TPut : class;

        Task<List<long>> AddOrUpdateRangeAsync<TPut>(IEnumerable<TPut> putObjects, bool shouldSave = false)
            where TPut : class;

        Task<bool> DeleteAsync<TDelete>(TDelete deleteObject, bool shouldSave = false) where TDelete : class;
        Task<bool> DeleteAsync(long id, bool shouldSave = false);

        Task<bool> DeleteRangeAsync<TDelete>(IEnumerable<TDelete> deleteObjects, bool shouldSave = false)
            where TDelete : class;

        Task<bool> DeleteRangeAsync(IEnumerable<long> ids, bool shouldSave = false);
        Task<bool> DisableAsync<TDisable>(TDisable deleteObject, bool shouldSave = false) where TDisable : class;
        Task<bool> DisableAsync(long id, bool shouldSave = false);

        Task<bool> DisableRangeAsync<TDisable>(IEnumerable<TDisable> deleteObjects, bool shouldSave = false)
            where TDisable : class;

        Task<bool> DisableRangeAsync(IEnumerable<long> ids, bool shouldSave = false);
    }
}
