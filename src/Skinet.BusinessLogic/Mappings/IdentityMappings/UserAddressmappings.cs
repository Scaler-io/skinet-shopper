using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.Entities.Entities.Identity;


namespace Skinet.BusinessLogic.Mappings.IdentityMappings
{
    public class UserAddressMappings : Profile
    {
        public UserAddressMappings()
        {
            CreateMap<Address, UserAddressDto>().ReverseMap();
        }
    }
}
