using System.Reflection;
using AutoMapper;

namespace FurnitureStore.Application.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly) =>
        ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();
    }
}
