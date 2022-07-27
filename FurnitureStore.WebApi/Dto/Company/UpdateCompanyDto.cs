using AutoMapper;
using FurnitureStore.Application.Common.Mappings;

namespace FurnitureStore.WebApi.Dto.Company;

public class UpdateCompanyDto : IMapWith<UpdateCompanyDto>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCompanyDto, UpdateCompanyDto>()
            .ForMember(c => c.Name,
                o => o.MapFrom(c => c.Name));
    }
}
