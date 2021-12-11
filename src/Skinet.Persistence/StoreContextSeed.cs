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
            var isProductsAvailable = await context.Products.AnyAsync();

            if (!isProductsAvailable)
            {
                await context.Products.AddRangeAsync(GetBaseProducts(logger));
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbcontextName}", typeof(StoreContext).Name);
                return;
            }
            
            logger.LogInformation("Nothing to migrate. Db is already up", typeof(StoreContext).Name);
        }

        private static IEnumerable<Product> GetBaseProducts(ILogger<StoreContextSeed> logger)
        {
            IEnumerable<Product> dummyProducts = null;
            using (var reader = new StreamReader("./SeedData/products.json"))
            {
                try
                {
                    var temp = reader.ReadToEnd();
                    dummyProducts = JsonConvert.DeserializeObject<List<Product>>(temp);
                    logger.LogInformation("Seed data loaded from products collection");
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Unable to load product data from products.json file");
                }
            }

            return dummyProducts;
        } 
    }
}
