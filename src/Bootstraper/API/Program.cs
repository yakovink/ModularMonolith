


var builder = WebApplication.CreateBuilder(args);

// Services container

builder.Services.AddControllers();
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);



var app = builder.Build();

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
