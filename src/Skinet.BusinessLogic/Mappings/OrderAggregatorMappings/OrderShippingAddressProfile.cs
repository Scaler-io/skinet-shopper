using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Mappings.OrderAggregatorMappings
{
    public class OrderShippingAddressProfile : Profile
    {
        public OrderShippingAddressProfile()
        {
            CreateMap<UserAddressDto, Address>().ReverseMap();
        }
    }
}