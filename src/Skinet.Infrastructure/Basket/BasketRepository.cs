using Skinet.BusinessLogic.Contracts.Infrastructure;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Skinet.Entities.Entities;

namespace Skinet.Infrastructure.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _basketDb;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _basketDb = connection.GetDatabase();
        }
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basketData = await _basketDb.StringGetAsync(basketId);
            return basketData.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basketData);
        }
        public async Task<CustomerBasket> UpsertBasketAsync(CustomerBasket basket)
        {
            var created = await _basketDb.StringSetAsync(basket.Id, 
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(10));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketDb.KeyDeleteAsync(basketId);
        }


    }
}
