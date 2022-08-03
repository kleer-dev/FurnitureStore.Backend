using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Delete;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public DeleteCompanyCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.Company> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, 
        CancellationToken cancellationToken)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.Id);

        _dbContext.Companies.Remove(company);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.RemoveCacheValue(request.Id);

        return Unit.Value;
    }
}
