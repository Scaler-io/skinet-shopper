using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.BusinessLogic.Core;

namespace Skinet.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class BaseControllerv1 : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound(result);
            if (result.Value == null && result.IsSuccess) return NotFound();
            if (result.Value != null && result.IsSuccess) return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
