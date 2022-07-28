using AutoMapper;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.GetList;

public class FurnitureTypeDto : IMapWith<Domain.FurnitureType>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.FurnitureType, FurnitureTypeDto>()
            .ForMember(f => f.Id,
                o => o.MapFrom(f => f.Id))
            .ForMember(f => f.Name, 
                o => o.MapFrom(f => f.Name));
    }
}
