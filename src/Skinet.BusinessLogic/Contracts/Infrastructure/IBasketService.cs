using Skinet.BusinessLogic.Core;
using Skinet.Entities.Entities;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IBasketService
    {
        Task<Result<CustomerBasket>> GetBasketAsync(string basketId);
        Task<Result<CustomerBasket>> UpsertBasketAsync(CustomerBasket basket);
        Task<Result<bool>> DeleteBasketAsync(string basketId);
    }
}
