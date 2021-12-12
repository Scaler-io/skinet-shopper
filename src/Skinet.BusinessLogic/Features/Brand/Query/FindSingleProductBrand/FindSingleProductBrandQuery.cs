using MediatR;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;

namespace Skinet.BusinessLogic.Features.Brand.Query.FindSingleProductBrand
{
    public class FindSingleProductBrandQuery : IRequest<Result<BrandToReturnDto>>
    {
        public int Id { get; set; }
    }
}
