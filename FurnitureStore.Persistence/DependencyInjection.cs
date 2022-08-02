using FurnitureStore.Application.Interfaces;
using FurnitureStore.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        var logConnectionString = configuration["LogDbConnection"];

        services.AddDbContext<FurnitureStoreDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddDbContext<LogDbContext>(options =>
        {
            options.UseNpgsql(logConnectionString);
        });

        services.AddScoped<IFurnitureStoreDbContext>(provider => 
            provider.GetService<FurnitureStoreDbContext>()!);
        
        services.AddScoped<ILogDbContext>(provider => 
            provider.GetService<LogDbContext>()!);

        return services;
    }
}
    