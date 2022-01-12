using Skinet.BusinessLogic.Core;
using Skinet.Entities.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IOrderService
    {
        Task<Result<Order>> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
        Task<Result<IReadOnlyList<Order>>> GetOrdersForUserAsync(string buyersEmail);
        Task<Result<Order>> GetOrderByIdAsync(int orderId, string buyerEmail);
        Task<Result<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsAsync();
    }
}
