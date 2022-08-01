using AutoMapper;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.Get;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public GetCompanyQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.Company> cacheManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<CompanyVm> Handle(GetCompanyQuery request,
        CancellationToken cancellationToken)
    {
        var companyQuery = async () => await _dbContext.Companies
            .Include(f => f.Furnitures)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        var company = await _cacheManager.GetOrSetCacheValue(request.Id, companyQuery);

        return _mapper.Map<CompanyVm>(company);
    }
}