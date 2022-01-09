using System.Collections.Generic;

namespace Skinet.BusinessLogic.Core.Dtos.Basket
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
