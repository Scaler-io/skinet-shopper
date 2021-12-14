using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Error;

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
            if (result == null) return NotFound(new ApiResponse(404));
            if (result.Value == null && result.IsSuccess) return NotFound(new ApiResponse(404));
            if (result.Value != null && result.IsSuccess) return Ok(result.Value);

            return BadRequest(new ApiResponse(400, result.Error));
        }
    }
}
