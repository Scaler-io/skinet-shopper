using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct
{
    public class FindSingleProductQueryHandler : IRequestHandler<FindSingleProductQuery, Result<ProductToReturnDto>>
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<FindSingleProductQueryHandler> _logger;

        public FindSingleProductQueryHandler(IProductRepository productRepo, IMapper mapper, ILogger<FindSingleProductQueryHandler> logger)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ProductToReturnDto>> Handle(FindSingleProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductWithBrandAndTypeSpecification(request.Id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            if(product == null)
            {
                _logger.LogError("No product was found with the product id {Id}", request.Id);
                return null;
            }

            var result = _mapper.Map<ProductToReturnDto>(product);

            return Result<ProductToReturnDto>.Success(result);
        }
    }
}
