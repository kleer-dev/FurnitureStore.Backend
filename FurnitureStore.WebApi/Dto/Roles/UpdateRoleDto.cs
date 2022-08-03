using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Role.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Roles;

public class UpdateRoleDto : IMapWith<UpdateRoleCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateRoleDto, UpdateRoleCommand>()
            .ForMember(r => r.Name,
                o => o.MapFrom(r => r.Name));
    }
}