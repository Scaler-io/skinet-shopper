using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Skinet.BusinessLogic.Features.Products.Query.GetAllProducts;
using Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Microsoft.Extensions.Logging;
using Skinet.Shared.LoggerExtensions;
using Microsoft.AspNetCore.Authorization;

namespace Skinet.API.Controllers.v1
{
    [ApiVersion("1")]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(typeof(Pagination<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "returns all products", typeof(Pagination<ProductToReturnDto>))]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecParams productParams)
        {
            _logger.Here().Controller(typeof(ProductController).Name, nameof(GetAllProducts)).HttpGet();

            var query = new GetAllProductsQuery(productParams);
            var products = await Mediator.Send(query);

            _logger.Exited(typeof(ProductController).Name, nameof(GetAllProducts));
            return HandleResult(products);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDto>), (int)HttpStatusCode.OK)]
        [SwaggerResponseAttribute((int)HttpStatusCode.OK, "finds product by id", typeof(IEnumerable<ProductToReturnDto>))]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            _logger.Here().Controller(typeof(ProductController).Name, nameof(GetProductById)).WithId(id).HttpGet();

            var query = new FindSingleProductQuery { Id = id };
            var product = await Mediator.Send(query);

            _logger.Exited(typeof(ProductController).Name, nameof(GetProductById));

            return HandleResult(product);
        }


    }
}
