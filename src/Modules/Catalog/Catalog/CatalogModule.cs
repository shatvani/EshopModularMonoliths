using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Register all validators from this assembly (FluentValidation core / DI extensions)
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }
}

// FluentValidation.AspNetCore is depriecated as of FluentValidation v11.0.0.
// Install FluentValidation, FluentValidation.DependencyInjectionExtensions and Microsoft.AspNetCore.Http.Abstractions V2.3.0 
