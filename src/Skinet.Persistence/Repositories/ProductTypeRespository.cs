using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;

namespace Skinet.Persistence.Repositories
{
    public class ProductTypeRespository : BaseRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRespository(StoreContext context): base(context) { }
    }
}
