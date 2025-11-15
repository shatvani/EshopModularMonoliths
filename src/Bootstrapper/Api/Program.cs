var builder = WebApplication.CreateBuilder(args);

// Add services to the containers
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration); // Configure Catalog services

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCatalogModule().
    UseBasketModule().
    UseOrderingModule();

app.Run();
