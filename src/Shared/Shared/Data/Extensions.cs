using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        MigrateDatabaseAsync<TContext>(app.ApplicationServices);

        SeedData(app.ApplicationServices);

        return app;
    }

    private static void MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        dbContext.Database.Migrate();
    }

    private static void SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (var seeder in seeders)
        {
            seeder.SeedAll();
        }
    }
}
