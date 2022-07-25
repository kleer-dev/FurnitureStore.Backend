using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Create;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Company;

public class CreateCompanyDto : IMapWith<CreateCompanyCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCompanyDto, CreateCompanyCommand>()
            .ForMember(c => c.Name, 
                o => o.MapFrom(d => d.Name));
    }
}
