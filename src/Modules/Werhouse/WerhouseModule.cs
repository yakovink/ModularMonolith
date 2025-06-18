namespace Werhouse;


public static class WerhouseModule
{

    public static IServiceCollection AddWerhouseModule(this IServiceCollection services, IConfiguration configuration)
    {
        

        services.AddScoped<IWerhouseRepository, WerhouseRepository>();
        services.Decorate<IWerhouseRepository, CachedWerhouseRepository>();
        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


        services.AddDbContext<WerhouseDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });

        return ModuleObject.AddModule(services, configuration, typeof(WerhouseModule));
    }

    public static IApplicationBuilder UseWerhouseModule(this IApplicationBuilder app)
    {
        app=ModuleObject.useMigrate<WerhouseDbContext>(app);
        return ModuleObject.UseModule(app);
    }
}