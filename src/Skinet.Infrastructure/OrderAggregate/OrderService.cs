using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core;
using Skinet.Entities.Entities;
using Skinet.Entities.Entities.OrderAggregate;
using Skinet.Shared.LoggerExtensions;

namespace Skinet.Infrastructure.OrderAggregate
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitOfWork unitOfWork, IBasketService basketService, ILogger<OrderService> logger)
        {
            _logger = logger;
            _basketService = basketService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Order>> CreateOrderAsync(string buyerEmail,
            int deliveryMethodId,
            string basketId,
            Address shippingAddress)
        {
            _logger.Here(nameof(OrderService), nameof(CreateOrderAsync));

            var basket = (await _basketService.GetBasketAsync(basketId)).Value;

            _logger.WithBasketId(basket.Id).LogInformation("Cutomer's basket recieved");

            var orderItems = await GetOrderItems(basket);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            _logger.DeliverMethodOpted(deliveryMethod.ShortName);

            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            var finalOrder = await GenerateFinalOrderAsync(orderItems, buyerEmail, shippingAddress, deliveryMethod, subTotal);
            
           if(finalOrder == null){
               _logger.LogError("Order creation was failed");
               return Result<Order>.Failure("Something went wrong. Please try again or contact skinet support");
           }
    
            _logger.WithOrderId(finalOrder.Id).LogInformation("Final order is ready for checkout");

            await _basketService.DeleteBasketAsync(basketId);
            
            _logger.Exited();

            return Result<Order>.Success(finalOrder);
        }

        private async Task<Order> GenerateFinalOrderAsync(List<OrderItem> orderItems,
            string buyerEmail, 
            Address shippingAddress,
            DeliveryMethod deliveryMethod,
            decimal subTotal)
        {
            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subTotal); 
            _unitOfWork.Repository<Order>().Add(order); 
            var result = await _unitOfWork.Complete();
            if(result <= 0)return null;
            return order;
        }

        
        private async Task<List<OrderItem>> GetOrderItems(CustomerBasket basket)
        {
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            return items;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Entities.Entities.OrderAggregate.Order>> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IReadOnlyList<Entities.Entities.OrderAggregate.Order>>> GetOrdersForUserAsync(string buyersEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}
