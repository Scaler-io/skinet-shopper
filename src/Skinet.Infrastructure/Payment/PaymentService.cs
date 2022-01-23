using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.Basket;
using Skinet.Entities.Entities;
using Skinet.Entities.Entities.OrderAggregate;
using Skinet.Shared.LoggerExtensions;
using Stripe;
using Order = Skinet.Entities.Entities.OrderAggregate.Order;
using Product = Skinet.Entities.Entities.Product;

namespace Skinet.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        ILogger<PaymentService> _logger;
        private readonly IMapper _mapper;

        public PaymentService(IBasketService basketService,
            IUnitOfWork unitOfWork,
            IConfiguration config,
            ILogger<PaymentService> logger,
            IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }

        public async Task<Result<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            _logger.Here(nameof(PaymentService), nameof(CreateOrUpdatePaymentIntent));

            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = (await _basketService.GetBasketAsync(basketId)).Value;

            if(basket.Items.Count() == 0) { 
                _logger.WithBasketId(basketId).LogInformation("No basket was found in db");
                return Result<CustomerBasketDto>.Failure($"No basket was found with id {basketId}");
            }

            var shippingPrice = 0m;
            
            if(basket.DeliveryMethodId.HasValue){
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                    .GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = deliveryMethod.Price;
            }
            
            foreach(var item in basket.Items){
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != productItem.Price){
                    item.Price = productItem.Price;
                }
            }
        
            PaymentIntent intent = new PaymentIntent();

            CreateOrUpdateNewIntentAsync(ref basket, intent, shippingPrice);
            
            await _basketService.UpsertBasketAsync(basket);

            var result = _mapper.Map<CustomerBasketDto>(basket);

            _logger.Exited();

            return Result<CustomerBasketDto>.Success(result);

        }

        private void CreateOrUpdateNewIntentAsync (ref CustomerBasket basket, PaymentIntent intent, decimal shippingPrice){
            _logger.Here(nameof(PaymentService), nameof(CreateOrUpdateNewIntentAsync)).LogInformation("Updating basket with payment info");
            
            var service = new PaymentIntentService();

            if(string.IsNullOrEmpty(basket.PaymentIntentId)){
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long) basket.Items.Sum(i => (i.Price * 100 ) * i.Quantity) + (long) (shippingPrice * 100),
                    Currency = "inr",
                    PaymentMethodTypes = new List<string> {"card"} 
                };

                intent = service.CreateAsync(options).Result;
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else{
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) basket.Items.Sum(i => (i.Price * 100 ) * i.Quantity) + (long) (shippingPrice * 100)
                };

                service.UpdateAsync(basket.PaymentIntentId, options);
            }

            _logger.WithBasketId(basket.Id).WithData(basket).LogInformation($"Basket updated with payment intent {intent.Id}");
        }

        public async Task<Result<Order>> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            _logger.Here(nameof(PaymentService), nameof(UpdateOrderPaymentFailed));

            var spec = new OrderByPaymentIntentIdWithItemsSpecification(paymentIntentId); 
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if(order == null) {
                _logger.WithOrderId(order.Id).LogError("Order was not found");
                return null;
            }

            order.Status = OrderStatus.PaymentFailed;
            _unitOfWork.Repository<Order>().Update(order);

            _logger.WithOrderId(order.Id).LogInformation("Payment faild. Order status upadeted");

             await _unitOfWork.Complete();
    
            _logger.Exited();

            return Result<Order>.Failure("Payment failed. Please try again later");
        }

        public async Task<Result<Order>> UpdateOrderPaymentSucceed(string paymentIntentId)
        {
            _logger.Here(nameof(PaymentService), nameof(UpdateOrderPaymentSucceed));

            var spec = new OrderByPaymentIntentIdWithItemsSpecification(paymentIntentId); 
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if(order == null) {
                _logger.WithOrderId(order.Id).LogError("Order was not found");
                return null;
            }

            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.Repository<Order>().Update(order);

            _logger.WithOrderId(order.Id).LogInformation("Payment recived. Order status upadeted");

            await _unitOfWork.Complete();
    
            _logger.Exited();

            return Result<Order>.Success(order);
        }
    }
}