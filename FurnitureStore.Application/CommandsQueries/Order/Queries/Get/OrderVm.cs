using AutoMapper;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.Get;

public class OrderVm : IMapWith<Domain.Order>
{
    public long Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Domain.Furniture Furniture { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Order, OrderVm>()
            .ForMember(order => order.Id,
                o => o.MapFrom(order => order.Id))
            .ForMember(order => order.IsCompleted,
                o => o.MapFrom(order => order.IsCompleted))
            .ForMember(order => order.CompletionDate,
                o => o.MapFrom(order => order.CompletionDate))
            .ForMember(order => order.UserName,
                o => o.MapFrom(order => order.User.UserName))
            .ForMember(order => order.Email,
                o => o.MapFrom(order => order.User.Email))
            .ForMember(order => order.PhoneNumber,
                o => o.MapFrom(order => order.User.PhoneNumber))
            .ForMember(order => order.Furniture,
                o => o.MapFrom(order => order.Furniture));
    }
}
