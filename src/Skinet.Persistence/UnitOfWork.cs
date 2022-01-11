using System;
using System.Collections;
using System.Threading.Tasks;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Common;
using Skinet.Persistence.Repositories;

namespace Skinet.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type)){
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(
                typeof(TEntity)), _context);
                
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>) _repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

         public void Dispose()
        {
            _context.Dispose();
        }
    }
}