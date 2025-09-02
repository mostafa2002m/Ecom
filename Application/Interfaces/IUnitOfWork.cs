using Application.Interfaces.Authentication;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategory CategoryRepo { get; }
        IProduct ProductRepo { get; }
        IPhoto photoRepo { get; }
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task BeginTransactionAsync();
    }
}
