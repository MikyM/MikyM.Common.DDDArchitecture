using AutoMapper;
using MikyM.Common.Utilities.Results;

namespace MikyM.Common.Application.Services;

/// <inheritdoc cref="IDataServiceBase{TContext}"/>
public abstract class DataServiceBase<TContext> : IDataServiceBase<TContext> where TContext : DbContext
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWork<TContext> UnitOfWork;
    private bool _disposed;

    protected DataServiceBase(IMapper mapper, IUnitOfWork<TContext> uof)
    {
        Mapper = mapper;
        UnitOfWork = uof;
    }

    public virtual async Task<Result<int>> CommitAsync(string? auditUserId)
    {
        return await UnitOfWork.CommitAsync(auditUserId);
    }

    public virtual async Task<Result<int>> CommitAsync()
    {
        return await UnitOfWork.CommitAsync();
    }

    public virtual async Task<Result> RollbackAsync()
    {
        await UnitOfWork.RollbackAsync();
        return Result.FromSuccess();
    }

    public virtual async Task<Result> BeginTransactionAsync()
    {
        await UnitOfWork.UseTransaction();
        return Result.FromSuccess();
    }

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) UnitOfWork.Dispose();

        _disposed = true;
    }
}