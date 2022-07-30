using AutoMapper;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.Application.CommandsQueries.User.Queries.Get;

public class UserVm : IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public long Balance { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, UserVm>()
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email))
            .ForMember(u => u.PhoneNumber,
                o => o.MapFrom(u => u.PhoneNumber))
            .ForMember(u => u.Balance,
                o => o.MapFrom(u => u.Balance));
    }
}