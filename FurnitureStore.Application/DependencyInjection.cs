using System.Reflection;
using FluentValidation;
using FurnitureStore.Application.Common.Behaviors;
using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton<IMemoryCache, MemoryCache>();
        services.AddScoped(typeof(ICacheManager<>), typeof(CacheManager<>));

        return services;
    }
}
