

using System.Reflection;

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



    public static IApplicationBuilder UseModule (IApplicationBuilder app)
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

