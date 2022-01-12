using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Mappings.OrderAggregatorMappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        }
    }
}