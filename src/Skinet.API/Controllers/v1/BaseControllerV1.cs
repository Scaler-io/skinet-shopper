using Microsoft.AspNetCore.Mvc;

namespace Skinet.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class BaseControllerv1 : ControllerBase
    {
    }
}
