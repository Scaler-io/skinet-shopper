using System;
using System.Threading.Tasks;
using Skinet.Entities.Common;

namespace Skinet.BusinessLogic.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}