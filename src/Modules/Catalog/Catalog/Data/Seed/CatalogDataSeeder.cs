using Shared.Data.Seed;

namespace Catalog.Data.Seed;

public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
{
    public void SeedAll()
    {
        if (!dbContext.Products.Any())
        {
            dbContext.Products.AddRange(InitialData.Products);
            dbContext.SaveChanges();
        }
    }
}
