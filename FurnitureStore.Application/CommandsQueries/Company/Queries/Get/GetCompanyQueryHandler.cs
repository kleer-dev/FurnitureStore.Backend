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

    public GetCompanyQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CompanyVm> Handle(GetCompanyQuery request,
        CancellationToken cancellationToken)
    {
        var company = await _dbContext.Companies
            .Include(f => f.Furnitures)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.Id);

        return _mapper.Map<CompanyVm>(company);
    }
}
