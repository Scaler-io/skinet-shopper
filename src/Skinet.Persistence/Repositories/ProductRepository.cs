using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace Skinet.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<Product> GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
