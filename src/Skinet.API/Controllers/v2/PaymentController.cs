using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.Shared.LoggerExtensions;

namespace Skinet.API.Controllers.v2
{

   [ApiVersion("2")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent([FromRoute] string basketId){
            _logger.Here().Controller(nameof(PaymentController), nameof(CreateOrUpdatePaymentIntent)).HttpPost();
            var result =  await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            _logger.Exited();
            return HandleResult(result);
        }

    }
}