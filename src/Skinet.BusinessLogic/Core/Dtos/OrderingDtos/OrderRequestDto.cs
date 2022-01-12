using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;

namespace Skinet.BusinessLogic.Core.Dtos.OrderingDtos
{
    public class OrderRequestDto
    {
        public string basketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public UserAddressDto ShippingAddress { get; set; }
        
    }
}