using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Update;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public UpdateCompanyCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.Id);

        company.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
