using AutoMapper;
using MikyM.Common.DataAccessLayer.Specifications;
using MikyM.Common.Utilities.Results;
using MikyM.Common.Utilities.Results.Errors;

namespace MikyM.Common.Application.Services;

/// <summary>
/// Read-only data service
/// </summary>
/// <inheritdoc cref="IReadOnlyDataService{TEntity,TContext}"/>
public class ReadOnlyDataService<TEntity, TContext> : DataServiceBase<TContext>, IReadOnlyDataService<TEntity, TContext>
    where TEntity : AggregateRootEntity where TContext : DbContext
{
    public ReadOnlyDataService(IMapper mapper, IUnitOfWork<TContext> uof) : base(mapper, uof)
    {
    }

    /// <inheritdoc />
    public virtual async Task<Result<TGetResult>> GetAsync<TGetResult>(bool shouldProject = false, params object[] keyValues) where TGetResult : class
    {
        var res = await this.GetAsync(keyValues);
        return !res.IsDefined() ? Result<TGetResult>.FromError(new NotFoundError()) : Result<TGetResult>.FromSuccess(this.Mapper.Map<TGetResult>(res.Entity));
    }

    /// <inheritdoc />
    public virtual async Task<Result<TEntity>> GetAsync(params object[] keyValues)
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>().GetAsync(keyValues);
        return res is null ? Result<TEntity>.FromError(new NotFoundError()) : Result<TEntity>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<TEntity>> GetSingleBySpecAsync(ISpecification<TEntity> specification)
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetSingleBySpecAsync(specification);
        return res is null ? Result<TEntity>.FromError(new NotFoundError()) : Result<TEntity>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<TGetResult>> GetSingleBySpecAsync<TGetResult>(ISpecification<TEntity> specification) where TGetResult : class
    {
        var res = await this.GetSingleBySpecAsync(specification);
        return !res.IsDefined(out var entity) ? Result<TGetResult>.FromError(new NotFoundError()) : Result<TGetResult>.FromSuccess(this.Mapper.Map<TGetResult>(entity));
    }

    /// <inheritdoc />
    public virtual async Task<Result<TGetProjectedResult>> GetSingleBySpecAsync<TGetProjectedResult>(ISpecification<TEntity, TGetProjectedResult> specification) where TGetProjectedResult : class
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetSingleBySpecAsync(specification);
        return res is null ? Result<TGetProjectedResult>.FromError(new NotFoundError()) : Result<TGetProjectedResult>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<TEntity>>> GetBySpecAsync(ISpecification<TEntity> specification)
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetBySpecAsync(specification);
        return Result<IReadOnlyList<TEntity>>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<TGetResult>>> GetBySpecAsync<TGetResult>(
        ISpecification<TEntity> specification) where TGetResult : class
    {
        var res = await this.GetBySpecAsync(specification);
        return res.IsDefined()
            ? Result<IReadOnlyList<TGetResult>>.FromError(new NotFoundError())
            : Result<IReadOnlyList<TGetResult>>.FromSuccess(this.Mapper.Map<IReadOnlyList<TGetResult>>(res.Entity));
    }

    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<TGetProjectedResult>>> GetBySpecAsync<TGetProjectedResult>(ISpecification<TEntity, TGetProjectedResult> specification) where TGetProjectedResult : class
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetBySpecAsync(specification);
        return Result<IReadOnlyList<TGetProjectedResult>>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<TGetResult>>> GetAllAsync<TGetResult>(bool shouldProject = false) where TGetResult : class
    {
        IReadOnlyList<TGetResult> res;
        if (shouldProject) res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetAllAsync<TGetResult>();
        else res = this.Mapper.Map<IReadOnlyList<TGetResult>>(await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetAllAsync());

        return Result<IReadOnlyList<TGetResult>>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>()
            .GetAllAsync();

        return Result<IReadOnlyList<TEntity>>.FromSuccess(res);
    }

    /// <inheritdoc />
    public virtual async Task<Result<long>> LongCountAsync(ISpecification<TEntity>? specification = null)
    {
        var res = await this.UnitOfWork.GetRepository<IReadOnlyRepository<TEntity>>().LongCountAsync(specification);
        return Result<long>.FromSuccess(res);
    }
}