using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;
using Skinet.BusinessLogic.Core.Error;
using Skinet.Entities.Entities.OrderAggregate;
using Skinet.Shared.LoggerExtensions;

namespace Skinet.API.Controllers.v2
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto request)
        {
            _logger.Here().Controller(nameof(OrderController), nameof(CreateOrder)).HttpPost();

            var email = HttpContext.User?.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<Address>(request.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(email, request.DeliveryMethodId, request.basketId, address);

            _logger.Exited();

            return HandleResult(order);
        }
    }
}