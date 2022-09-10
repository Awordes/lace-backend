using System.Reflection;
using Lace.Application;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Симбирский шнурок",
        Version = "v1"
    });

    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml"));
});

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lace v1");
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
    endpoints.MapDefaultControllerRoute();
});

app.Run();