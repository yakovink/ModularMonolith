var builder = WebApplication.CreateBuilder(args);

// Services container

var app = builder.Build();

// Connection pipeline

app.Run();
