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
    public class TypeController : BaseControllerv1
    {
        private readonly IAsyncRepository<ProductType> _productTypeRepo;

        public TypeController(IAsyncRepository<ProductType> productTypeRepo)
        {
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet(Name = "GetAllProductTypes")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all product types", typeof(IEnumerable<ProductType>))]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrands()
        {
            var types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}
