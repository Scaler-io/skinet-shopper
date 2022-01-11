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

        IAsyncRepository<Order> _orderRepo;
        IAsyncRepository<DeliveryMethod> _deliveryRepo;
        IAsyncRepository<Product> _productRepo;
        IBasketService _basketService;
        ILogger<OrderService> _logger;

        public OrderService(IAsyncRepository<Order> orderRepo,
            IAsyncRepository<DeliveryMethod> deliveryRepo,
            IAsyncRepository<Product> productRepo,
            IBasketService basketService, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _deliveryRepo = deliveryRepo;
            _productRepo = productRepo;
            _basketService = basketService;
            _logger = logger;
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

            var deliveryMethod = await _deliveryRepo.GetByIdAsync(deliveryMethodId);

            _logger.DeliverMethodOpted(deliveryMethod.ShortName);
            
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
            
            var finalOrder = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subTotal);

            _logger.WithOrderId(finalOrder.Id).LogInformation("Final order is ready for checkout");
            _logger.Exited();

            return Result<Order>.Success(finalOrder);
        }

        private async Task<List<OrderItem>> GetOrderItems(CustomerBasket basket){
            var items = new List<OrderItem>();
            
            foreach(var item in basket.Items){
                var productItem = await _productRepo.GetByIdAsync(item.Id);
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
