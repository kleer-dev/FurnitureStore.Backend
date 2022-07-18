using FurnitureStore.Application.Interfaces;
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

        services.AddDbContext<FurnitureStoreDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IFurnitureStoreDbContext>(provider => 
            provider.GetService<FurnitureStoreDbContext>());

        return services;
    }
}
    