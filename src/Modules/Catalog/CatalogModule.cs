

namespace Catalog;

public static class CatalogModule 
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        //Type catalogType=typeof(CatalogModule);
        services.AddScoped<IHttpController, CatalogModuleStructre.CatalogHttpController>();
        // Add services to the container.
        services.AddScoped<ICatalogRepository, CatalogLocalRepository>();
        //services.Decorate<ICatalogRepository, CatalogModuleStructre.CachedCatalogRepository>();
        // API Endpoint Services

        // Application use cases services

        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();
        
        services.AddDbContext<CatalogDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });
        services.AddScoped<GenericDbContext<CatalogDbContext>>(sp => sp.GetRequiredService<CatalogDbContext>());


        services.AddScoped<IGenericRepository<Product>, GenericRepository<Product, CatalogDbContext>>();
        services.Decorate<IGenericRepository<Product>, GenericCachedRepository<Product, CatalogDbContext>>();

        //services.AddScoped<IDataSeeder, CatalogDataSeed>();
        return ModuleObject.AddModule(services,configuration, typeof(CatalogModule));
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {   
        app=ModuleObject.useMigrate<CatalogDbContext>(app);
        return ModuleObject.UseModule(app);
    }
}
