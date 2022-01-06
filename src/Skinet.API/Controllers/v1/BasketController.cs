using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Core.Dtos.Basket;
using Skinet.BusinessLogic.Core.Error;
using Skinet.Entities.Entities;
using Skinet.Shared.LoggerExtensions;
using System.Net;
using System.Threading.Tasks;

namespace Skinet.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger, IMapper mapper)
        {
            _basketService = basketService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBasketById([FromRoute]string id)
        {
            _logger.Here().Controller(nameof(BasketController), nameof(GetBasketById)).HttpGet();

            var result = await _basketService.GetBasketAsync(id);
            
            _logger.Exited();
            
            return HandleResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] CustomerBasketDto basket)
        {
            _logger.Here().Controller(nameof(BasketController), nameof(CreateOrUpdateBasket)).HttpPost();

            var customerBasket = _mapper.Map<CustomerBasket>(basket);

            var result = await _basketService.UpsertBasketAsync(customerBasket);

            _logger.Exited();

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExistingBasket([FromRoute] string id)
        {
            _logger.Here().Controller(nameof(BasketController), nameof(DeleteExistingBasket)).HttpPost();

            var result = await _basketService.DeleteBasketAsync(id);

            _logger.Exited();

            return HandleResult(result);
        }

    }
}
