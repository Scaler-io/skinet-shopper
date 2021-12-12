using MediatR;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;

namespace Skinet.BusinessLogic.Features.Type.Query.FindSingleProductType
{
    public class FindSingleProductTypeQuery : IRequest<Result<TypesToReturnDto>>
    {
        public int Id { get; set; }
    }
}
