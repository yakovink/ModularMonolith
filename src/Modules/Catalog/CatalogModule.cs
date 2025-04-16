



using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catalog;


public static class CatalogModule 
{

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        // API Endpoint Services

        // Application use cases services

        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

        // Data - infrastructure services

        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


        services.AddDbContext<CatalogDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });

        services.AddScoped<IDataSeeder, CatalogDataSeed>();
        return ModuleObject.AddModule(services,configuration, typeof(CatalogModule));
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {   
        app=ModuleObject.useMigrate<CatalogDbContext>(app);
        return ModuleObject.UseModule(app);
    }


}
