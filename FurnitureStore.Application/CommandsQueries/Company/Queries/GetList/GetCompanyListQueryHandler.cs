using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.GetList;

public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, GetCompanyListVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompanyListQueryHandler(IFurnitureStoreDbContext dbContext, 
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetCompanyListVm> Handle(GetCompanyListQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _dbContext.Companies
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetCompanyListVm() { Companies = companies };
    }
}
