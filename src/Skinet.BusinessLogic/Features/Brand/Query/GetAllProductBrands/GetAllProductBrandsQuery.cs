using MediatR;
using Skinet.BusinessLogic.Core;
using System.Collections.Generic;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;

namespace Skinet.BusinessLogic.Features.Brand.Query.GetAllProductBrands
{
    public class GetAllProductBrandsQuery : IRequest<Result<IReadOnlyList<BrandToReturnDto>>>
    {
    }
}
