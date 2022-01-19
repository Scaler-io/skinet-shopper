using System.Collections.Generic;

namespace Skinet.Entities.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(){}

        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
