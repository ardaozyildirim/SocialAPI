using System;
using System.Threading.Tasks;

namespace SocialAPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
} 