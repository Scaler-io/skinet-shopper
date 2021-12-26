using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Features.Products.Query.GetAllProducts;
using Skinet.BusinessLogic.Mappings.ProductMappings;
using Skinet.Persistence.Repositories;

namespace Skinet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // api versioning settings
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            // binds data repository services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRespository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            //  MediatR support
            services.AddMediatR(typeof(GetAllProductsQuery).Assembly);

            // automapper support
            services.AddAutoMapper(typeof(ProductMapping).Assembly);

            // Cors support
            services.AddCors(opt =>
            {
                opt.AddPolicy("SkinetCorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}
