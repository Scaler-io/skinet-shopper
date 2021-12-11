using Skinet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> GetAllProducts();
        Task<IReadOnlyList<Product>> GetAllProductsByName(string productName);
    }
}
