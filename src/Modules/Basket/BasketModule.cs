




namespace Basket;

public static class BasketModule
{

    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        /*
        services.AddScoped<IBasketRepository>(provider=>
            new CachedBasketRepository(
                provider.GetRequiredService<IBasketRepository>(),
                provider.GetRequiredService<IDistributedCache>())
        );
        */



        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


        services.AddDbContext<BasketDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });


        return ModuleObject.AddModule(services, configuration, typeof(BasketModule));
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {

        app = ModuleObject.useMigrate<BasketDbContext>(app);
        return ModuleObject.UseModule(app);
    }

}
