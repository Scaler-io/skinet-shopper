using Microsoft.AspNetCore.Mvc;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;
using Skinet.BusinessLogic.Features.Type.Query.FindSingleProductType;
using Skinet.BusinessLogic.Features.Type.Query.GetAllProductTypes;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Skinet.API.Controllers.v1
{
    public class TypeController : BaseControllerv1
    {
        [HttpGet(Name = "GetAllProductTypes")]
        [ProducesResponseType(typeof(IReadOnlyList<TypesToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all product types", typeof(IEnumerable<TypesToReturnDto>))]
        public async Task<IActionResult> GetAllProductBrands()
        {
            var query = new GetAllProductTypesQuery();
            var result = await Mediator.Send(query);

            return HandleResult(result);
        }

        [HttpGet("{id}", Name = "GetProductTypeById")]
        [ProducesResponseType(typeof(TypesToReturnDto), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "find single product type by id", typeof(TypesToReturnDto))]
        public async Task<IActionResult> GetProductTypeById(int id)
        {
            var query = new FindSingleProductTypeQuery { Id = id };
            var result = await Mediator.Send(query);

            return HandleResult(result);
        }
    }
}
