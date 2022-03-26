using AutoMapper;
using MikyM.Common.Utilities.Results;
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace MikyM.Common.Application.Services;

/// <summary>
/// CRUD data service
/// </summary>
/// <inheritdoc cref="ICrudDataService{TEntity,TContext}"/>
public class CrudDataService<TEntity, TContext> : ReadOnlyDataService<TEntity, TContext>, ICrudDataService<TEntity, TContext>
    where TEntity : AggregateRootEntity where TContext : DbContext
{
    /// <summary>
    /// Creates a new instance of <see cref="CrudDataService{TEntity,TContext}"/>
    /// </summary>
    /// <param name="mapper">Mapper instance</param>
    /// <param name="uof">Unit of work instance</param>
    public CrudDataService(IMapper mapper, IUnitOfWork<TContext> uof) : base(mapper, uof)
    {
    }

    /// <inheritdoc />
    public virtual async Task<Result<long>> AddAsync<TPost>(TPost entry, bool shouldSave = false, string? userId = null) where TPost : class
    {
        if (entry  is null) throw new ArgumentNullException(nameof(entry));

        TEntity entity;
        if (entry is TEntity rootEntity)
        {
            entity = rootEntity;
            UnitOfWork.GetRepository<IRepository<TEntity>>().Add(entity);
        }
        else
        {
            entity = Mapper.Map<TEntity>(entry);
            UnitOfWork.GetRepository<IRepository<TEntity>>().Add(entity);
        }

        if (!shouldSave) return 0;

        await CommitAsync(userId);
        return Result<long>.FromSuccess(entity.Id);
    }

    /// <inheritdoc />
    public virtual async Task<Result<IEnumerable<long>>> AddRangeAsync<TPost>(IEnumerable<TPost> entries,
        bool shouldSave = false, string? userId = null) where TPost : class
    {
        if (entries  is null) throw new ArgumentNullException(nameof(entries));

        List<TEntity> entities;

        if (entries is IEnumerable<TEntity> rootEntities)
        {
            entities = rootEntities.ToList();
            UnitOfWork.GetRepository<IRepository<TEntity>>().AddRange(entities);
        }
        else
        {
            entities = Mapper.Map<List<TEntity>>(entries);
            UnitOfWork.GetRepository<IRepository<TEntity>>().AddRange(entities);
        }

        if (!shouldSave) return new List<long>();

        await CommitAsync(userId);
        return Result<IEnumerable<long>>.FromSuccess(entities.Select(e => e.Id).ToList());
    }

    /// <inheritdoc />
    public virtual Result BeginUpdate<TPatch>(TPatch entry, bool shouldSwapAttached = false) where TPatch : class
    {
        switch (entry)
        {
            case null:
                throw new ArgumentNullException(nameof(entry));
            case TEntity rootEntity:
                UnitOfWork.GetRepository<IRepository<TEntity>>().BeginUpdate(rootEntity, shouldSwapAttached);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>().BeginUpdate(Mapper.Map<TEntity>(entry), shouldSwapAttached);
                break;
        }

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual Result BeginUpdateRange<TPatch>(IEnumerable<TPatch> entries, bool shouldSwapAttached = false) where TPatch : class
    {
        switch (entries)
        {
            case null:
                throw new ArgumentNullException(nameof(entries));
            case IEnumerable<TEntity> rootEntities:
                UnitOfWork.GetRepository<IRepository<TEntity>>().BeginUpdateRange(rootEntities, shouldSwapAttached);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>()
                    .BeginUpdateRange(Mapper.Map<IEnumerable<TEntity>>(entries), shouldSwapAttached);
                break;
        }

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DeleteAsync<TDelete>(TDelete entry, bool shouldSave = false, string? userId = null) where TDelete : class
    {
        switch (entry)
        {
            case null:
                throw new ArgumentNullException(nameof(entry));
            case TEntity rootEntity:
                UnitOfWork.GetRepository<IRepository<TEntity>>().Delete(rootEntity);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>().Delete(Mapper.Map<TEntity>(entry));
                break;
        }

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DeleteAsync(long id, bool shouldSave = false, string? userId = null)
    {
        UnitOfWork.GetRepository<IRepository<TEntity>>().Delete(id);

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DeleteRangeAsync(IEnumerable<long> ids, bool shouldSave = false, string? userId = null)
    {
        if (ids  is null) throw new ArgumentNullException(nameof(ids));

        UnitOfWork.GetRepository<IRepository<TEntity>>().DeleteRange(ids);

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DeleteRangeAsync<TDelete>(IEnumerable<TDelete> entries, bool shouldSave = false, string? userId = null)
        where TDelete : class
    {
        switch (entries)
        {
            case null:
                throw new ArgumentNullException(nameof(entries));
            case IEnumerable<TEntity> rootEntities:
                UnitOfWork.GetRepository<IRepository<TEntity>>().DeleteRange(rootEntities);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>()
                    .DeleteRange(Mapper.Map<IEnumerable<TEntity>>(entries));
                break;
        }

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DisableAsync(long id, bool shouldSave = false, string? userId = null)
    {
        await UnitOfWork.GetRepository<IRepository<TEntity>>()
            .DisableAsync(id);

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DisableAsync<TDisable>(TDisable entry, bool shouldSave = false, string? userId = null) where TDisable : class
    {
        switch (entry)
        {
            case null:
                throw new ArgumentNullException(nameof(entry));
            case TEntity rootEntity:
                UnitOfWork.GetRepository<IRepository<TEntity>>().Disable(rootEntity);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>().Disable(Mapper.Map<TEntity>(entry));
                break;
        }

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DisableRangeAsync(IEnumerable<long> ids, bool shouldSave = false, string? userId = null)
    {
        if (ids  is null) throw new ArgumentNullException(nameof(ids));

        await UnitOfWork.GetRepository<IRepository<TEntity>>()
            .DisableRangeAsync(ids);

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> DisableRangeAsync<TDisable>(IEnumerable<TDisable> entries, bool shouldSave = false, string? userId = null)
        where TDisable : class
    {
        switch (entries)
        {
            case null:
                throw new ArgumentNullException(nameof(entries));
            case IEnumerable<TEntity> rootEntities:
                UnitOfWork.GetRepository<IRepository<TEntity>>().DisableRange(rootEntities);
                break;
            default:
                UnitOfWork.GetRepository<IRepository<TEntity>>()
                    .DisableRange(Mapper.Map<IEnumerable<TEntity>>(entries));
                break;
        }

        if (shouldSave) 
            await CommitAsync(userId);

        return Result.FromSuccess();
    }
}