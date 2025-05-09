




var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCarter(typeof(CatalogModule).Assembly);


// Services container

builder.Services.AddControllers();
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);



var app = builder.Build();

app.MapCarter();


// Connection pipeline

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();



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
