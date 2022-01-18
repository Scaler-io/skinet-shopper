using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.BusinessLogic.Core.Error;
using Skinet.Shared.LoggerExtensions;
using System.Net;
using System.Threading.Tasks;

namespace Skinet.API.Controllers.v2
{
    [ApiVersion("2")]
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("signin")]
        [ProducesResponseType(typeof(AuthResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignInUser([FromBody] SigninRequestDto request)
        {
            _logger.Here().Controller(nameof(AccountController), nameof(SignInUser)).HttpPost();
                
            var result = await _authService.SigninAsync(request);

            _logger.Exited();

            return HandleResult(result);
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(AuthResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SignUpUser([FromBody] SignupRequestDto request)
        {
            _logger.Here().Controller(nameof(AccountController), nameof(SignUpUser)).HttpPost();

            if(CheckEmailExist(request.Email).Result.Value)
            {
                return UnprocessableEntity(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Email is already taken "}
                });
            }


            var result = await _authService.SignupAsync(request);

            _logger.Exited();

            return HandleResult(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(AuthResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCurrentUser()
        {
            _logger.Here().Controller(nameof(AccountController), nameof(GetCurrentUser)).HttpGet();

            var result = await _authService.GetAuthUserAsync();

            _logger.Exited();

            return HandleResult(result);
        }
   
        [HttpGet("IsEmailTaken")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            return await _authService.CheckEmailExistsAsync(email);
        }

        [Authorize]
        [HttpGet("address")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUserAddress()
        {
            _logger.Here().Controller(nameof(AccountController), nameof(GetUserAddress)).HttpGet();

            var result = await _authService.GetUserAddressAsync();

            
            _logger.Exited();

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPut("address")]
        [ProducesResponseType(typeof(UserAddressDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiException), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUserAddress([FromBody] UserAddressDto address)
        {
            _logger.Here().Controller(nameof(AccountController), nameof(UpdateUserAddress)).HttpGet();

            var result = await _authService.UpdateAddressAsync(address);

            _logger.Exited();

            return HandleResult(result);
        }
    }
}
