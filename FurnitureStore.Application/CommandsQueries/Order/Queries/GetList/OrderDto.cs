using AutoMapper;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Domain;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.GetList;

public class OrderDto : IMapWith<Domain.Order>
{
    public long Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }

    public User User { get; set; }
    public Domain.Furniture Furniture { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Order, OrderDto>()
            .ForMember(order => order.Id,
                o => o.MapFrom(order => order.Id))
            .ForMember(order => order.IsCompleted,
                o => o.MapFrom(order => order.IsCompleted))
            .ForMember(order => order.CompletionDate,
                o => o.MapFrom(order => order.CompletionDate))
            .ForMember(order => order.User,
                o => o.MapFrom(order => order.User))
            .ForMember(order => order.Furniture,
                o => o.MapFrom(order => order.Furniture));
    }
}
