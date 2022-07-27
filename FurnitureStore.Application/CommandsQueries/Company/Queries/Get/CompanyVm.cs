using AutoMapper;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Domain;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.Get;

public class CompanyVm : IMapWith<Domain.Company>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Furniture> Furnitures { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Company, CompanyVm>()
            .ForMember(c => c.Id,
                o => o.MapFrom(c => c.Id))
            .ForMember(c => c.Name,
                o => o.MapFrom(c => c.Name))
            .ForMember(c => c.Furnitures,
                o => o.MapFrom(c => c.Furnitures));
    }
}
