using System.Collections.Generic;
using MediatR;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;

namespace Skinet.BusinessLogic.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Result<Pagination<ProductToReturnDto>>>
    {
        public GetAllProductsQuery(ProductSpecParams productParams)
        {
            ProductParams = productParams;
        }

        public ProductSpecParams ProductParams { get; set; }
    }
}
