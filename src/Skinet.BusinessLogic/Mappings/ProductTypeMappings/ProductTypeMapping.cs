using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings.ProductTypeMappings
{
    public class ProductTypeMapping : Profile
    {
        public ProductTypeMapping()
        {
            CreateMap<ProductType, TypesToReturnDto>()
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
