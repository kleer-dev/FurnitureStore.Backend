using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Update;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Company;

public class UpdateCompanyDto : IMapWith<UpdateCompanyCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCompanyDto, UpdateCompanyCommand>()
            .ForMember(c => c.Name,
                o => o.MapFrom(c => c.Name));
    }
}
