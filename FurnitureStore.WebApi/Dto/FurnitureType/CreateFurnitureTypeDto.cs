using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Create;
using FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.WebApi.Dto.Company;

namespace FurnitureStore.WebApi.Dto.FurnitureType;

public class CreateFurnitureTypeDto : IMapWith<CreateFurnitureTypeCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFurnitureTypeDto, CreateFurnitureTypeCommand>()
            .ForMember(c => c.Name,
                o => o.MapFrom(d => d.Name));
    }
}
