using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Core.Dtos.Basket;
using Skinet.BusinessLogic.Core.Error;
using Skinet.Shared.LoggerExtensions;
using Stripe;
using Order = Skinet.Entities.Entities.OrderAggregate.Order;

namespace Skinet.API.Controllers.v2
{

   [ApiVersion("2")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string whSecret = "whsec_TUOMt1ZXAgQ36MHf8hxbR3JbDsNHlWPZ";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        [ProducesResponseType(typeof(CustomerBasketDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent([FromRoute] string basketId){
            _logger.Here().Controller(nameof(PaymentController), nameof(CreateOrUpdatePaymentIntent)).HttpPost();
            var result =  await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            _logger.Exited();
            return HandleResult(result);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook(){
            _logger.Here().Controller("PyamnetController", "StripeWebHook").HttpPost();

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                    whSecret);
                PaymentIntent paymentIntent;
                Order order;

                switch(stripeEvent.Type){
                    case Events.PaymentIntentSucceeded:
                        paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment Succeeded: ", paymentIntent.Id);
                        order = (await _paymentService.UpdateOrderPaymentSucceed(paymentIntent.Id)).Value;
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment Failed: ", paymentIntent.Id);
                        order = (await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id)).Value;
                        break;
                    default:
                        _logger.LogInformation("Unhandled event: {0}", stripeEvent.Type);
                        break;
                }

                _logger.Exited();
                
                return new EmptyResult();
        }

    }
}