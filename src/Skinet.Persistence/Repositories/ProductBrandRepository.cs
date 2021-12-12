using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Repositories
{
    public class ProductBrandRepository : BaseRepository<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(StoreContext context) : base(context) { }
    }
}
