using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;
using Skinet.Entities.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Type.Query.FindSingleProductType
{
    public class FindSingleProductTypeQueryHandler : IRequestHandler<FindSingleProductTypeQuery, Result<TypesToReturnDto>>
    {
        private readonly IAsyncRepository<ProductType> _productTypeRepo;
        private readonly ILogger<FindSingleProductTypeQueryHandler> _logger;
        private readonly IMapper _mapper;

        public FindSingleProductTypeQueryHandler(IAsyncRepository<ProductType> productTypeRepo, 
            ILogger<FindSingleProductTypeQueryHandler> logger, 
            IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<TypesToReturnDto>> Handle(FindSingleProductTypeQuery request, CancellationToken cancellationToken)
        {
            var productType = await _productTypeRepo.GetByIdAsync(request.Id);
            var result = _mapper.Map<TypesToReturnDto>(productType);

            return Result<TypesToReturnDto>.Success(result);
        }
    }
}
