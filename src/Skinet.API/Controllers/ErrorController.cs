using Microsoft.AspNetCore.Mvc;
using Skinet.BusinessLogic.Core.Error;

namespace Skinet.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Error1Controller : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
