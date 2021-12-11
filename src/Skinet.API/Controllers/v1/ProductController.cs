using Microsoft.AspNetCore.Mvc;
using Skinet.Entities.Entities;
using System.Collections.Generic;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using Skinet.BusinessLogic.Contracts.Persistence;
using System.Threading.Tasks;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Features.Products.Query.GetAllProducts;
using Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct;

namespace Skinet.API.Controllers.v1
{
    public class ProductController : BaseControllerv1
    {
        private readonly IAsyncRepository<Product> _productRepo;

        public ProductController(IAsyncRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all products", typeof(IEnumerable<Product>))]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await Mediator.Send(query);
            return HandleResult(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "finds product by id", typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            var query = new FindSingleProductQuery { Id = id };
            var product = await Mediator.Send(query);
            return HandleResult(product);
        }


    }
}
