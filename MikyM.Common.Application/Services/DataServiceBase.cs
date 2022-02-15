using AutoMapper;
using MikyM.Common.DataAccessLayer.UnitOfWork;
using MikyM.Common.Utilities.Results;

namespace MikyM.Common.Application.Services;

/// <summary>
/// Base data service
/// </summary>
/// <inheritdoc cref="IDataServiceBase{TContext}"/>
public abstract class DataServiceBase<TContext> : IDataServiceBase<TContext> where TContext : DbContext
{
    /// <summary>
    /// <see cref="IMapper"/> instance
    /// </summary>
    protected readonly IMapper Mapper;
    /// <summary>
    /// <see cref="IUnitOfWork{TContext}"/> instance
    /// </summary>
    protected readonly IUnitOfWork<TContext> UnitOfWork;
    private bool _disposed;

    protected DataServiceBase(IMapper mapper, IUnitOfWork<TContext> uof)
    {
        Mapper = mapper;
        UnitOfWork = uof;
    }

    /// <inheritdoc />
    public virtual async Task<Result<int>> CommitAsync(string? auditUserId)
    {
        return await UnitOfWork.CommitAsync(auditUserId);
    }

    /// <inheritdoc />
    public virtual async Task<Result<int>> CommitAsync()
    {
        return await UnitOfWork.CommitAsync();
    }

    /// <inheritdoc />
    public virtual async Task<Result> RollbackAsync()
    {
        await UnitOfWork.RollbackAsync();
        return Result.FromSuccess();
    }

    /// <inheritdoc />
    public virtual async Task<Result> BeginTransactionAsync()
    {
        await UnitOfWork.UseTransaction();
        return Result.FromSuccess();
    }

    // Public implementation of Dispose pattern callable by consumers.
    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    /// <summary>
    /// Dispose action
    /// </summary>
    /// <param name="disposing">Whether disposing</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) UnitOfWork.Dispose();

        _disposed = true;
    }
}