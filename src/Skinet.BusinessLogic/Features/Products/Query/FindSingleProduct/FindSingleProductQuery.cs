using MediatR;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;

namespace Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct
{
    public class FindSingleProductQuery : IRequest<Result<ProductToReturnDto>>
    {
        public int Id { get; set; }
    }
}
