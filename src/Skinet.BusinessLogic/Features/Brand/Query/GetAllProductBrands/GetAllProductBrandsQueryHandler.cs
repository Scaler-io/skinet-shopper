using AutoMapper;
using MediatR;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.BrandDtos;
using Skinet.Entities.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Brand.Query.GetAllProductBrands
{
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, Result<IReadOnlyList<BrandToReturnDto>>>
    {
        private readonly IAsyncRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public GetAllProductBrandsQueryHandler(IAsyncRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandToReturnDto>>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands =  await _productBrandRepo.ListAllAsync();
            var result = _mapper.Map<IReadOnlyList<BrandToReturnDto>>(brands);
            return Result<IReadOnlyList<BrandToReturnDto>>.Success(result);
        }
    }
}
