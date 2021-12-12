using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context) { }
    }
}
