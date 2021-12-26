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
    }
}
