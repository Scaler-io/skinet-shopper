using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.Basket;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings.BasketMappings
{
    public class CustomerBasketMappings : Profile
    {
        public CustomerBasketMappings()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
