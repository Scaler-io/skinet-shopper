using System.Threading.Tasks;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.Basket;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<Result<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId);
    }
}