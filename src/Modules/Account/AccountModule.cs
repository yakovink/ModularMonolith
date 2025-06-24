
namespace Account;

public static class AccountModule
{
    public static IServiceCollection AddAccountModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHttpController, AccountModuleStructre.AccountHttpController>();

        services.AddScoped<IAccountRepository, AccountLocalRepository>();
        //services.Decorate<IAccountRepository, AccountModuleStructre.CachedAccountRepository>();

        string? configurationString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        services.AddDbContext<AccountDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
            options.UseNpgsql(configurationString);
        });
        services.AddScoped<GenericDbContext<AccountDbContext>>(sp => sp.GetRequiredService<AccountDbContext>());


        services.AddScoped<IGenericRepository<User>, GenericRepository<User, AccountDbContext>>();
        services.Decorate<IGenericRepository<User>, GenericCachedRepository<User, AccountDbContext>>();

        return ModuleObject.AddModule(services, configuration, typeof(AccountModule));
    }

    public static IApplicationBuilder UseAccountModule(this IApplicationBuilder app)
    {
        app = ModuleObject.useMigrate<AccountDbContext>(app);
        return ModuleObject.UseModule(app);
    }
}