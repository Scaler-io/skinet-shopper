using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.BusinessLogic.Contracts.Infrastructure;
using Skinet.BusinessLogic.Contracts.Persistence;
using Skinet.BusinessLogic.Core.Error;
using Skinet.BusinessLogic.Features.Products.Query.GetAllProducts;
using Skinet.BusinessLogic.Mappings.ProductMappings;
using Skinet.BusinessLogic.Validators;
using Skinet.Infrastructure.Basket;
using Skinet.Infrastructure.Identity;
using Skinet.Persistence.Repositories;
using System.Linq;


namespace Skinet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddControllers();

            // api versioning settings
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            // binds data repository services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRespository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAuthService, AuthService>();

            //  MediatR support
            services.AddMediatR(typeof(GetAllProductsQuery).Assembly);

            services.AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssembly(typeof(UserAddressValidator).Assembly);
            });

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

            // api model error behavior settings
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .SelectMany(s => s.Value.Errors)
                                .Select(s => s.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new UnprocessableEntityObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
