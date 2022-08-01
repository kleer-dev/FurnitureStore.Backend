using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Update;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public UpdateCompanyCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.Company> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var isCompanyExist = await _dbContext.Companies
            .AnyAsync(c => c.Name == request.Name && c.Id != request.Id, cancellationToken);

        if (isCompanyExist)
            throw new RecordIsExistException(request.Name);

        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.Id);

        company.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, company);

        return Unit.Value;
    }
}
