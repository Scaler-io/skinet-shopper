using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.TypeDtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Type.Query.GetAllProductTypes
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, Result<IReadOnlyList<TypesToReturnDto>>>
    {
        private readonly IProductTypeRepository _productTypeRepo;
        private readonly ILogger<GetAllProductTypesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllProductTypesQueryHandler(IProductTypeRepository productTypeRepo, 
            ILogger<GetAllProductTypesQueryHandler> logger, 
            IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<TypesToReturnDto>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _productTypeRepo.ListAllAsync();

            if(productTypes == null)
            {
                _logger.LogError("No product types were found");
                return null;
            }

            var result = _mapper.Map<IReadOnlyList<TypesToReturnDto>>(productTypes);

            return Result<IReadOnlyList<TypesToReturnDto>>.Success(result);
        }
    }
}
