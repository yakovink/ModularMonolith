
namespace Shared.GenericRootModule;

public static class ModuleObject
    {
    public static IServiceCollection AddModule (IServiceCollection services,IConfiguration configuration, Type ModuleType)
    {
        /*
        services
            .AddTransient<IModuleService, ModuleService>()
            .AddScoped<IModuleRepository, ModuleRepository>();
            */
        // add carter servisces

        return services;

    }
    public static void RegisterCarter(this IServiceCollection services, params Assembly[] assemblies){


        services.AddCarter(configurator:config=>{
            foreach (var assembly in assemblies){
                var modules=assembly.GetTypes().Where(t=>t.IsAssignableTo(typeof(ICarterModule))).ToArray();
                config.WithModules(modules);
            }
        });    
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }

    public static IServiceCollection RegisterMassTransmit(this IServiceCollection services,IConfiguration configuration, params Assembly[] assemblies)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.SetInMemorySagaRepositoryProvider();
            config.AddConsumers(assemblies);
            config.AddSagaStateMachines(assemblies);
            config.AddSagas(assemblies);
            config.AddActivities(assemblies);
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]!);
                    host.Password(configuration["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });
        });


        return services;
    }



    public static IApplicationBuilder UseModule(IApplicationBuilder app)
    {
        return app;
    }
    public static IApplicationBuilder useMigrate<TDbContext>(IApplicationBuilder app) where TDbContext:DbContext
    {
        InitialiseDatabaseAsync<TDbContext>(app).GetAwaiter().GetResult();
        
        seedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();
        
        return app;
    }

    private static async Task seedDataAsync(IServiceProvider applicationServices) 
    {
        IServiceScope scope = applicationServices.CreateScope();
        IEnumerable<IDataSeeder> seeders=scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (IDataSeeder seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }

    private static async Task InitialiseDatabaseAsync<TDbContext>(IApplicationBuilder app) where TDbContext:DbContext
    {
        IServiceScope scope = app.ApplicationServices.CreateScope();
        TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await context.Database.MigrateAsync();
    }




}

