using AutoMapper;
using Microsoft.Extensions.Configuration;
using Skinet.BusinessLogic.Core.Dtos;
using Skinet.Entities.Entities;

namespace Skinet.BusinessLogic.Mappings
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember
            , ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
