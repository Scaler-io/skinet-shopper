using Microsoft.AspNetCore.Mvc;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.Entities.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Skinet.API.Controllers.v1
{
    public class BrandController : BaseControllerv1
    {
        private readonly IAsyncRepository<ProductBrand> _productBrandRepo;

        public BrandController(IAsyncRepository<ProductBrand> productBrandRepo)
        {
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet(Name = "GetAllProductBrands")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all product brands", typeof(IEnumerable<ProductBrand>))]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }
    }
}
