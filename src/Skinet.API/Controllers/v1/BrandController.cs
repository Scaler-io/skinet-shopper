using Microsoft.AspNetCore.Mvc;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;
using Skinet.BusinessLogic.Features.Brand.Query.FindSingleProductBrand;
using Skinet.BusinessLogic.Features.Brand.Query.GetAllProductBrands;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Skinet.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BrandController : BaseController
    {
        [HttpGet(Name = "GetAllProductBrands")]
        [ProducesResponseType(typeof(IReadOnlyList<BrandToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all product brands", typeof(IEnumerable<BrandToReturnDto>))]
        public async Task<IActionResult> GetAllProductBrands()
        {
            var query = new GetAllProductBrandsQuery();
            var brands = await Mediator.Send(query);
            return HandleResult(brands);
        }

        [HttpGet("{id}", Name = "GetProductBrandById")]
        [ProducesResponseType(typeof(BrandToReturnDto), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "finds single product brand by id", typeof(BrandToReturnDto))]
        public async Task<IActionResult> GetProductBrandById([FromRoute]int id)
        {
            var query = new FindSingleProductBrandQuery { Id = id };
            var brands = await Mediator.Send(query);
            return HandleResult(brands);
        }
    }
}
