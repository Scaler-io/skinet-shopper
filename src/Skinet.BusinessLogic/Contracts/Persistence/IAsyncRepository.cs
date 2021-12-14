using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.Entities.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
