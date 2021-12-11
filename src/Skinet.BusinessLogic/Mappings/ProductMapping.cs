using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductImageUrlResolver>());
        }
    }
}
