using AutoMapper;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.FurnitureType;

public class UpdateFurnitureTypeDto : IMapWith<UpdateFurnitureTypeCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateFurnitureTypeDto, UpdateFurnitureTypeCommand>()
            .ForMember(c => c.Name,
                o => o.MapFrom(d => d.Name));
    }
}
