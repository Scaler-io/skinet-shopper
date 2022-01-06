using AutoMapper;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.Entities.Entities.Identity;


namespace Skinet.BusinessLogic.Mappings.IdentityMappings
{
    public class UserAddressmappings : Profile
    {
        public UserAddressmappings()
        {
            CreateMap<Address, UserAddressDto>().ReverseMap();
        }
    }
}
