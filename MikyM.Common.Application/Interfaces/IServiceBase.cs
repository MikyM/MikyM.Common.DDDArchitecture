using System;
using System.Threading.Tasks;

namespace MikyM.Common.Application.Interfaces
{
    public interface IServiceBase : IDisposable
    {
        Task<int> CommitAsync();
        Task RollbackAsync();
        Task BeginTransactionAsync();
    }
}
