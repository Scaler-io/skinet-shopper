using Microsoft.EntityFrameworkCore;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyList<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsByName(string productName)
        {
            return await _context.Products.Where(p => p.Name == productName).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
