using System.ComponentModel.Design;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Catalog;

public static class CatalogModule 
{

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        return ModuleObject.AddModule(services,configuration, typeof(CatalogModule));
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return ModuleObject.UseModule(app);
    }

}
