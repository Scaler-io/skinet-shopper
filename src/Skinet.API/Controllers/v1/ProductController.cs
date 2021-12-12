using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Skinet.BusinessLogic.Features.Products.Query.GetAllProducts;
using Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;

namespace Skinet.API.Controllers.v1
{
    public class ProductController : BaseControllerv1
    {
        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all products", typeof(IEnumerable<ProductToReturnDto>))]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await Mediator.Send(query);
            return HandleResult(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "finds product by id", typeof(IEnumerable<ProductToReturnDto>))]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            var query = new FindSingleProductQuery { Id = id };
            var product = await Mediator.Send(query);
            return HandleResult(product);
        }


    }
}
