using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Shared;

namespace Basket;

public static class BasketModule
{

    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
    {
        return ModuleObject.AddModule(services,configuration, typeof(BasketModule));
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        return ModuleObject.UseModule(app);
    }

}
