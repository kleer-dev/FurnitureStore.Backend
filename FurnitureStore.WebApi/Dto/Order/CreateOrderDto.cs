using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Create;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Order;

public class CreateOrderDto : IMapWith<CreateOrderCommand>
{
    public long FurnitureId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
            .ForMember(order => order.FurnitureId,
                o => o.MapFrom(order => order.FurnitureId));
    }
}
