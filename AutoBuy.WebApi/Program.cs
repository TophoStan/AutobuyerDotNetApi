using AutobuyerPlayer;
using Data;
using Data.Contracts;
using Environment;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using GooglePlaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "AutoBuy API";
        s.Description = "AutoBuy API for managing brands, products, options, and order steps";
        s.Version = "v1";
    };
});

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = EnvironmentExtensions.GetJwtSigningKey()) //add this
    .AddAuthorization() //add this
    .AddFastEndpoints();

builder.Services
    .AddOpenApi()
    .AddHttpClient()
    .AddDatabase()
    .AddPlaywrightServerService()
    .AddGooglePlacesApi();

builder.Services.AddCors(x => { x.AddDefaultPolicy(y =>
{
    y.AllowAnyOrigin();
    y.AllowAnyHeader();
}); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerGen();
}

app.UseAuthentication() //add this
    .UseAuthorization() //add this
    .UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });
app.UseHttpsRedirection();


app.UseCors();

app.Run();