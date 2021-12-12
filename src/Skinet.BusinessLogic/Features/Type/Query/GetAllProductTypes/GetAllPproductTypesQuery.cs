using MediatR;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;
using System.Collections.Generic;

namespace Skinet.BusinessLogic.Features.Type.Query.GetAllProductTypes
{
    public class GetAllProductTypesQuery : IRequest<Result<IReadOnlyList<TypesToReturnDto>>>
    {
    }
}
