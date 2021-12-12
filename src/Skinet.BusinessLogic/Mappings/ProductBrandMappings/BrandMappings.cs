using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings.ProductBrandMappings
{
    public class BrandMappings : Profile
    {
        public BrandMappings()
        {
            CreateMap<ProductBrand, BrandToReturnDto>()
                .ForMember(d => d.Metadata, o => o.MapFrom(s => new MetaDataDto { 
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    LastModifiedAt = s.LastModifiedAt,
                    LastModifieddBy = s.LastModifiedBy
                }));
        }
    }
}
