using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Brand.Query.FindSingleProductBrand
{
    public class FindSingleProductBrandQueryHandler : IRequestHandler<FindSingleProductBrandQuery, Result<BrandToReturnDto>>
    {
        private readonly IProductBrandRepository _productBrandRepo;
        private readonly ILogger<FindSingleProductBrandQueryHandler> _logger;
        private readonly IMapper _mapper;

        public FindSingleProductBrandQueryHandler(IProductBrandRepository productBrandRepo, 
            ILogger<FindSingleProductBrandQueryHandler> logger, 
            IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<BrandToReturnDto>> Handle(FindSingleProductBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _productBrandRepo.GetByIdAsync(request.Id);
            if(brand == null)
            {
                _logger.LogError("No brand was found with brand id {Id}", request.Id);
                return null;
            }

            var result = _mapper.Map<BrandToReturnDto>(brand);

            return Result<BrandToReturnDto>.Success(result);
        }
    }
}
