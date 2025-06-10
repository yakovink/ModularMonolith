namespace Werhouse;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Shared.GenericRootModule;


public static class WerhouseModule
{

    public static IServiceCollection AddWerhouseModule(this IServiceCollection services, IConfiguration configuration)
    {
        return ModuleObject.AddModule(services,configuration, typeof(WerhouseModule));
    }

    public static IApplicationBuilder UseWerhouseModule(this IApplicationBuilder app)
    {
        return ModuleObject.UseModule(app);
    }
}