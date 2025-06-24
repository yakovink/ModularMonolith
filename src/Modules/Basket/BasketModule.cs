


namespace Basket;

public static class BasketModule
{

    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
    {



        services.AddScoped<IHttpController, BasketModuleStructre.BasketHttpController>();
        services.AddScoped<IBasketRepository, BasketLocalRepository>();



        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


        services.AddDbContext<BasketDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });
        services.AddScoped<GenericDbContext<BasketDbContext>>(sp => sp.GetRequiredService<BasketDbContext>());


        services.AddScoped<IGenericRepository<ShoppingCart>, GenericRepository<ShoppingCart, BasketDbContext>>();
        services.Decorate<IGenericRepository<ShoppingCart>, GenericCachedRepository<ShoppingCart, BasketDbContext>>();

        return ModuleObject.AddModule(services, configuration, typeof(BasketModule));
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {

        app = ModuleObject.useMigrate<BasketDbContext>(app);
        return ModuleObject.UseModule(app);
    }

}
