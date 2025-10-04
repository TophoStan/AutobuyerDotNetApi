using AutobuyerPlayer;
using Data;
using Data.Contracts;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "AutoBuy API";
        s.Description = "AutoBuy API for managing brands, products, options, and order steps";
        s.Version = "v1";
    };
});
builder.Services.AddPlaywrightServerService();
builder.Services.AddDbContext<AutoBuyAutoBuyDbContext>();
builder.Services.AddScoped<IAutoBuyDbContext,  AutoBuyAutoBuyDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerGen();
}

app.UseHttpsRedirection();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

app.Run();
