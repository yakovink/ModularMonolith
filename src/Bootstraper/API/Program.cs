











var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});


Assembly? catalogAssemly = typeof(CatalogModule).Assembly;
Assembly? basketAssembly = typeof(BasketModule).Assembly;
Assembly? accountAssembly = typeof(AccountModule).Assembly;

builder.Services.RegisterCarter(
    catalogAssemly,
    basketAssembly,
    accountAssembly
);

builder.Services.AddMediatR(
    cfg =>
    {

        cfg.RegisterServicesFromAssemblies(catalogAssemly,basketAssembly,accountAssembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));        
    });
builder.Services.AddValidatorsFromAssemblies([catalogAssemly,basketAssembly,accountAssembly]);



// Services container

builder.Services.AddControllers();
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    .AddAccountModule(builder.Configuration);


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();


// Connection pipeline

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule()
    .UseAccountModule();

app.UseExceptionHandler(options => { });


/*
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
*/
app.Run();
