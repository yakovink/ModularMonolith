



using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catalog;


public static class CatalogModule 
{

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {

        Type catalogType=typeof(CatalogModule);
        // Add services to the container.

        // API Endpoint Services

        // Application use cases services


        services.AddMediatR(
            cfg => {
                Console.WriteLine($"Scanning assembly: {Assembly.GetExecutingAssembly().FullName}");
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssembly(typeof(GenericRequest<>).Assembly);
                
            }
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
        return ModuleObject.AddModule(services,configuration, catalogType);
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {   
        app=ModuleObject.useMigrate<CatalogDbContext>(app);
        return ModuleObject.UseModule(app);
    }


}
