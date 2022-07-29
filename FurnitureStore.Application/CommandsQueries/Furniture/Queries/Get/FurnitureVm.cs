using AutoMapper;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;

public class FurnitureVm : IMapWith<Domain.Furniture>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Lenght { get; set; }
    public double Height { get; set; }
    public string Material { get; set; }

    public Domain.FurnitureType FurnitureType { get; set; }
    public Domain.Company Company { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Furniture, FurnitureVm>()
            .ForMember(f => f.Id,
                o => o.MapFrom(f => f.Id))
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
            .ForMember(f => f.FurnitureType,
                o => o.MapFrom(f => f.FurnitureType))
            .ForMember(f => f.Company,
                o => o.MapFrom(f => f.Company));
    }
}
