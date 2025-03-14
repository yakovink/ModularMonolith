using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Shared;

namespace Ordering;

public static class OrderingModule
{

    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
    {
        return ModuleObject.AddModule(services,configuration, typeof(OrderingModule));
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        return ModuleObject.UseModule(app);
    }
}
