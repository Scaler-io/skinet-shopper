using Skinet.Entities.Common;
using Skinet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<Product> GetProductByName(string productName);
    }
}
