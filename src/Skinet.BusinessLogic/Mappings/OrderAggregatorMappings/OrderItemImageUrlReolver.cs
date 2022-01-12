using AutoMapper;
using Microsoft.Extensions.Configuration;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.Entities.Entities.OrderAggregate; 

namespace Skinet.BusinessLogic.Mappings.OrderAggregatorMappings
{
    public class OrderItemImageUrlReolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemImageUrlReolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl)){
                return _configuration["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}