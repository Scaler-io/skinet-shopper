using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skinet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Skinet.Persistence
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILogger<StoreContextSeed> logger)
        {
            if (!await context.ProductBrands.AnyAsync())
            {
                await context.ProductBrands.AddRangeAsync(GetPreconfiguredProdctBrands(logger));
                await context.SaveChangesAsync();
            }

            if (!await context.ProductTypes.AnyAsync())
            {
                await context.ProductTypes.AddRangeAsync(GetPreconfiguredProductTypes(logger));
                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(GetBaseProducts(logger));
                await context.SaveChangesAsync();
            }  
            
            logger.LogInformation("Seed database associated with context {DbcontextName}", typeof(StoreContext).Name);
        }

        private static IEnumerable<ProductBrand> GetPreconfiguredProdctBrands(ILogger<StoreContextSeed> logger)
        {
            try
            {
                var brandsData = File.ReadAllText("../Skinet.Persistence/SeedData/brands.json");
                var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);
                logger.LogInformation("brands data loaded from brands.json file");
                return brands;
            }
            catch(Exception e)
            {
                logger.LogError(e, "Unable to load brands data from brands.json file");
            }

            return null;
        }

        private static IEnumerable<ProductType> GetPreconfiguredProductTypes(ILogger<StoreContextSeed> logger)
        {
            try
            {
                var productTypeData = File.ReadAllText("../Skinet.Persistence/SeedData/types.json");
                var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypeData);
                logger.LogInformation("types data loaded from types.json file");
                return productTypes;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unable to load types data from types.json file");
            }

            return null;
        }

        private static IEnumerable<Product> GetBaseProducts(ILogger<StoreContextSeed> logger)
        {
            try
            {
                var productsData = File.ReadAllText("../Skinet.Persistence/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);
                logger.LogInformation("products data loaded from products.json file");
                return products;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unable to load products data from products.json file");
            }

            return null;
        } 
    }
}
