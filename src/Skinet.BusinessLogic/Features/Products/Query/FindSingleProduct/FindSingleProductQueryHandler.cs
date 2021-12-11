using AutoMapper;
using MediatR;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.Entities.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Products.Query.FindSingleProduct
{
    public class FindSingleProductQueryHandler : IRequestHandler<FindSingleProductQuery, Result<ProductToReturnDto>>
    {
        private readonly IAsyncRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public FindSingleProductQueryHandler(IAsyncRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Result<ProductToReturnDto>> Handle(FindSingleProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductWithBrandAndTypeSpecification(request.Id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            var result = _mapper.Map<ProductToReturnDto>(product);

            return Result<ProductToReturnDto>.Success(result);
        }
    }
}
