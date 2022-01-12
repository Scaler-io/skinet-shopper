using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Mappings.OrderAggregatorMappings
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemImageUrlReolver>());
        }
    }
}