using FurnitureStore.Application.Interfaces;
using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Create;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, long>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public CreateCompanyCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = new Domain.Company
        {
            Name = request.Name
        };

        await _dbContext.Companies.AddAsync(company, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
