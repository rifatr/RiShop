using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infra.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infra/Data/DataSeeding/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands != null)
                    {
                        foreach (var brand in brands)
                        {
                            context.ProductBrands.Add(brand);
                        }

                        await context.SaveChangesAsync();
                    }
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infra/Data/DataSeeding/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types != null)
                    {
                        foreach (var type in types)
                        {
                            context.ProductTypes.Add(type);
                        }

                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infra/Data/DataSeeding/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products != null)
                    {
                        foreach (var product in products)
                        {
                            context.Products.Add(product);
                        }

                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}