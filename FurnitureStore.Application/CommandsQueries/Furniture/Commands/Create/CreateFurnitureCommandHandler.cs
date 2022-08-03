using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Create;

public class CreateFurnitureCommandHandler : IRequestHandler<CreateFurnitureCommand, long>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public CreateFurnitureCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(CreateFurnitureCommand request, 
        CancellationToken cancellationToken)
    {
        var furnitureType = await _dbContext.FurnitureTypes
            .FirstOrDefaultAsync(f => f.Id == request.FurnitureTypeId, cancellationToken);

        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(f => f.Id == request.CompanyId, cancellationToken);

        bool isExist = await _dbContext.Furnitures
            .AnyAsync(f => f.Name == request.Name, cancellationToken);

        if (furnitureType == null)
            throw new NotFoundException(nameof(Domain.FurnitureType), request.FurnitureTypeId);

        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.CompanyId);

        if (isExist)
            throw new RecordIsExistException(request.Name);

        var furniture = new Domain.Furniture() 
        {
            Name = request.Name,
            Price = request.Price,
            Lenght = request.Lenght,
            Height = request.Height,
            Material = request.Material,
            FurnitureType = furnitureType,
            Company = company
        };

        await _dbContext.Furnitures.AddAsync(furniture, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return furniture.Id;
    }
}
