using System.Reflection;
using Lace.Application.DbContexts;
using Lace.Application.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lace.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var laceDbContextOptions = configuration.GetSection(LaceDbContextOptions.SectionName).Get<LaceDbContextOptions>();
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddDbContextFactory<LaceDbContext>(builder =>
            builder.UseNpgsql(laceDbContextOptions.ConnectionString,
                b => { }));
        
        return services;
    }
}