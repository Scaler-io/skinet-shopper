using Skinet.BusinessLogic.Contracts.Infrastructure;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Skinet.Entities.Entities;
using Skinet.BusinessLogic.Core;
using Microsoft.Extensions.Logging;
using Skinet.Shared.LoggerExtensions;

namespace Skinet.Infrastructure.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IDatabase _basketDb;
        private readonly ILogger _logger;

        public BasketService(IConnectionMultiplexer connection, ILogger<BasketService> logger)
        {
            _basketDb = connection.GetDatabase();
            _logger = logger;
        }
        public async Task<Result<CustomerBasket>> GetBasketAsync(string basketId)
        {
            _logger.Here(nameof(GetBasketAsync), typeof(BasketService).Name);

            var basketData = await _basketDb.StringGetAsync(basketId);

            if (basketData.IsNullOrEmpty)
            {
                _logger.WithId(basketId).LogInformation($"New empty basket created");
                return Result<CustomerBasket>.Success(new CustomerBasket(basketId));
            }

            var result = JsonSerializer.Deserialize<CustomerBasket>(basketData);

            _logger.Exited(nameof(GetBasketAsync), typeof(BasketService).Name);

            return Result<CustomerBasket>.Success(result);
        }
        public async Task<Result<CustomerBasket>> UpsertBasketAsync(CustomerBasket basket)
        {
            _logger.Here(nameof(UpsertBasketAsync), typeof(BasketService).Name);

            var created = await _basketDb.StringSetAsync(basket.Id, 
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(10));

            if (!created)
            {
                _logger.WithId(basket.Id).LogError("No basket was created");
                return null;
            }

            _logger.Exited(nameof(UpsertBasketAsync), typeof(BasketService).Name);

            return await GetBasketAsync(basket.Id);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId)
        {
            _logger.Here(nameof(DeleteBasketAsync), typeof(BasketService).Name);

            var isDeleted =  await _basketDb.KeyDeleteAsync(basketId);

            if (!isDeleted)
            {
                _logger.WithId(basketId).LogError($"Basket deletion was failed");
                return Result<bool>.Failure("Basket deleteion was failed");
            }

            _logger.Exited(nameof(DeleteBasketAsync), typeof(BasketService).Name);

            return Result<bool>.Success(isDeleted);
        }
    }
}
