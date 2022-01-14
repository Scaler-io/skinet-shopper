using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
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
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IBasketService basketService, ILogger<OrderService> logger, IMapper mapper)
        {
            _logger = logger;
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OrderResponseDto>> CreateOrderAsync(string buyerEmail,
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
               return Result<OrderResponseDto>.Failure("Something went wrong. Please try again or contact skinet support");
           }

            var response = _mapper.Map<OrderResponseDto>(finalOrder);

            _logger.WithOrderId(finalOrder.Id).LogInformation("Final order is ready for checkout");

            await _basketService.DeleteBasketAsync(basketId);
            
            _logger.Exited();

            return Result<OrderResponseDto>.Success(response);
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

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodsAsync()
        {
            _logger.Here(nameof(OrderService), nameof(GetDeliveryMethodsAsync));

            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();

            var result = _mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods);

            _logger.Exited();
            return Result<IReadOnlyList<DeliveryMethodDto>>.Success(result);
        }

        public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            _logger.Here(nameof(OrderService), nameof(GetOrderByIdAsync));
            _logger.WithOrderId(orderId).LogInformation($"Searching for orders of user {buyerEmail}");

            var spec = new OrderWithItemsAndOrderingSpecification(orderId, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            var result = _mapper.Map<OrderResponseDto>(order);

            _logger.LogInformation("Serach result sent back to calling method");
            _logger.Exited();

            return Result<OrderResponseDto>.Success(result);
        }

        public async Task<Result<IReadOnlyList<OrderResponseDto>>> GetOrdersForUserAsync(string buyersEmail)
        {
            _logger.Here(nameof(OrderService), nameof(GetOrdersForUserAsync));
            _logger.LogInformation($"Searching for orders of user {buyersEmail}");
            
            var spec = new OrderWithItemsAndOrderingSpecification(buyersEmail);

            var orders = await _unitOfWork.Repository<Order>().ListAsync(spec);

            var result = _mapper.Map<IReadOnlyList<OrderResponseDto>>(orders);
            
            _logger.LogInformation("Serach result sent back to calling method");
            _logger.Exited();

            return Result<IReadOnlyList<OrderResponseDto>>.Success(result);
        }
    }
}
