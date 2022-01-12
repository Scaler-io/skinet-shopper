using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.Entities.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Contracts.Infrastructure
{
    public interface IOrderService
    {
        Task<Result<OrderResponseDto>> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
        Task<Result<IReadOnlyList<OrderResponseDto>>> GetOrdersForUserAsync(string buyersEmail);
        Task<Result<OrderResponseDto>> GetOrderByIdAsync(int orderId, string buyerEmail);
        Task<Result<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsAsync();
    }
}
