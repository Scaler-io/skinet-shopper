using System.Threading.Tasks;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.Basket;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<Result<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId);
        Task<Result<Order>> UpdateOrderPaymentSucceed(string paymentIntentId);
        Task<Result<Order>> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}