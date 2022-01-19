using System.Collections.Generic;

namespace Skinet.BusinessLogic.Core.Dtos.Basket
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
