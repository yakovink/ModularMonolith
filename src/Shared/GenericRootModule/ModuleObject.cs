using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

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
        
        return services;

    }

    public static IApplicationBuilder UseModule (IApplicationBuilder app)
    {
        return app;
    }
}

