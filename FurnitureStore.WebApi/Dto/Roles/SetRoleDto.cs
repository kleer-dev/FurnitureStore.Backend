using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Role.Commands.SetRole;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Roles;

public class SetRoleDto : IMapWith<SetRoleCommand>
{
    public long UserId { get; set; }
    public string RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleDto, SetRoleCommand>()
            .ForMember(r => r.UserId,
                o => o.MapFrom(r => r.UserId))
            .ForMember(r => r.RoleId,
                o => o.MapFrom(r => r.RoleId));
    }
}