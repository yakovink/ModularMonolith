
namespace Catalog;


public static class CatalogModule 
{

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        string? configurationString=configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CatalogDbContext> (options=>options.UseNpgsql(configurationString));
        return ModuleObject.AddModule(services,configuration, typeof(CatalogModule));
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return ModuleObject.UseModule(app);
    }

}
