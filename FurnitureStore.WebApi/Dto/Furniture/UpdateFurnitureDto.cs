using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Furniture.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Furniture;

public class UpdateFurnitureDto : IMapWith<UpdateFurnitureCommand>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Lenght { get; set; }
    public double Height { get; set; }
    public string Material { get; set; }

    public long furnitureTypeId { get; set; }
    public long companyId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateFurnitureDto, UpdateFurnitureCommand>()
            .ForMember(f => f.Name,
                o => o.MapFrom(f => f.Name))
            .ForMember(f => f.Price,
                o => o.MapFrom(f => f.Price))
            .ForMember(f => f.Lenght,
                o => o.MapFrom(f => f.Lenght))
            .ForMember(f => f.Height,
                o => o.MapFrom(f => f.Height))
            .ForMember(f => f.Material,
                o => o.MapFrom(f => f.Material))
            .ForMember(f => f.furnitureTypeId,
                o => o.MapFrom(f => f.furnitureTypeId))
            .ForMember(f => f.companyId,
                o => o.MapFrom(f => f.companyId));
    }
}
