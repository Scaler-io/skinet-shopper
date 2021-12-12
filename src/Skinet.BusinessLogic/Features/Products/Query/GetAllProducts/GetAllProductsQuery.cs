using System.Collections.Generic;
using MediatR;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;

namespace Skinet.BusinessLogic.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Result<IReadOnlyList<ProductToReturnDto>>>
    {
    }
}
