﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Error;

namespace Skinet.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound(new ApiResponse(404));
            if (result.Value == null && result.IsSuccess) return NotFound(new ApiResponse(404));
            if (result.Value != null && result.IsSuccess) return Ok(result.Value);

            if (result.Error == "Unauthorised") return Unauthorized(new ApiResponse(401));

            return BadRequest(new ApiResponse(400, result.Error));
        }
    }
}
