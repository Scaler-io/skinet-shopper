using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Mappings.OrderAggregatorMappings
{
    public class DeliveryMethodProfile : Profile
    {
        public DeliveryMethodProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>()
                .ForMember(d => d.MetaData, o => o.MapFrom(s => new MetaDataDto {
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    LastModifiedAt = s.LastModifiedAt,
                    LastModifieddBy = s.LastModifiedBy 
                }));
        }
    }
}