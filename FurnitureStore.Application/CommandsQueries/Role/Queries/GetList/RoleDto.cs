using AutoMapper;
using FurnitureStore.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Application.CommandsQueries.Role.Queries.GetList;

public class RoleDto : IMapWith<IdentityRole<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IdentityRole<long>, RoleDto>()
            .ForMember(r => r.Id,
                o => o.MapFrom(r => r.Id))
            .ForMember(r => r.Name,
                o => o.MapFrom(r => r.Name));
    }
}