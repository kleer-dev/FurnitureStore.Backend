using AutoMapper;
using FurnitureStore.Application.CommandsQueries.User.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.User;

public class UpdateUserDto : IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.UserName))
            .ForMember(u => u.PhoneNumber,
                o => o.MapFrom(u => u.PhoneNumber))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email))
            .ForMember(u => u.OldPassword,
                o => o.MapFrom(u => u.OldPassword))
            .ForMember(u => u.NewPassword,
                o => o.MapFrom(u => u.NewPassword));
    }
}