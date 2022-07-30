using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Order.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Order;

public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
{
    public long FurnitureId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
            .ForMember(order => order.FurnitureId,
                o => o.MapFrom(order => order.FurnitureId));
    }
}
