using AutoMapper;
using MediatR;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.BusinessLogic.Core;
using Skinet.BusinessLogic.Core.Dtos.ProductDtos;
using Skinet.Entities.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Skinet.BusinessLogic.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<Pagination<ProductToReturnDto>>>
    {
        private readonly IAsyncRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IAsyncRepository<Product> productRepo,
                IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<Result<Pagination<ProductToReturnDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productParams = request.ProductParams;
            var spec = new ProductWithBrandAndTypeSpecification(productParams);

            var countSpec = new ProductWithFilterForCountSpecification(productParams); 

            var totalItems = await _productRepo.CountAsync(countSpec);
            
            var products = await _productRepo.ListAsync(spec);

            var result =  _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Result<Pagination<ProductToReturnDto>>.Success(new Pagination<ProductToReturnDto>(
                 productParams.PageIndex, productParams.PageSize, totalItems, result
            ));
        }
    }
}
