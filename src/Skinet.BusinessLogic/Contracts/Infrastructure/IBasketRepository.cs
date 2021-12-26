using Skinet.Entities.Entities;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpsertBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
