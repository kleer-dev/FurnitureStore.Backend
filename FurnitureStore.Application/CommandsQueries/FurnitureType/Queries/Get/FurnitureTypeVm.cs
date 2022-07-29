using AutoMapper;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Domain;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class FurnitureTypeVm : IMapWith<Domain.FurnitureType>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Domain.Furniture> Furnitures { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.FurnitureType, FurnitureTypeVm>()
            .ForMember(f => f.Id,
                o => o.MapFrom(f => f.Id))
            .ForMember(f => f.Name,
                o => o.MapFrom(f => f.Name))
            .ForMember(f => f.Furnitures,
                o => o.MapFrom(f => f.Furnitures));
    }
}
