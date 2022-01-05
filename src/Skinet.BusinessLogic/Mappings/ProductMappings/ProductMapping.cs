using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings.ProductMappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductImageUrlResolver>())
                .ForMember(d => d.Metadata, o => o.MapFrom(s => new MetaDataDto
                {
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    LastModifiedAt = s.LastModifiedAt,
                    LastModifieddBy = s.LastModifiedBy
                }));                         
        }
    }
}
