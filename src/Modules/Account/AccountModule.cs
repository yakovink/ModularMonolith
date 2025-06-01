

namespace Account;



public static class AccountModule
{

    public static IServiceCollection AddAccountModule(this IServiceCollection services, IConfiguration configuration)
    {
        
        string? configurationString=configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


        services.AddDbContext<AccountDbContext> ((sp,options)=>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });


        return ModuleObject.AddModule(services, configuration, typeof(AccountModule));
    }

    public static IApplicationBuilder UseAccountModule(this IApplicationBuilder app)
    {

        app = ModuleObject.useMigrate<AccountDbContext>(app);
        return ModuleObject.UseModule(app);
    }

}