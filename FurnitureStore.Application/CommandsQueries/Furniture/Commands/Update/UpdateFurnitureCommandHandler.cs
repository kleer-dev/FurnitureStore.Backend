using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Update;

public class UpdateFurnitureCommandHandler : IRequestHandler<UpdateFurnitureCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public UpdateFurnitureCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateFurnitureCommand request,
        CancellationToken cancellationToken)
    {
        var furniture = await _dbContext.Furnitures
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        var furnitureType = await _dbContext.FurnitureTypes
            .FirstOrDefaultAsync(f => f.Id == request.furnitureTypeId, cancellationToken);

        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == request.companyId, cancellationToken);

        bool isFurnitureExist = await _dbContext.Furnitures
               .AnyAsync(f => f.Name == request.Name && f.Id != request.Id, cancellationToken);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.Id);
        if (furnitureType == null)
            throw new NotFoundException(nameof(Domain.FurnitureType), request.furnitureTypeId);
        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.companyId);
        if (isFurnitureExist)
            throw new RecordIsExistException(request.Name);

        furniture.Name = request.Name;
        furniture.Price = request.Price;
        furniture.Lenght = request.Lenght;
        furniture.Height = request.Height;
        furniture.Material = request.Material;
        furniture.FurnitureType = furnitureType;
        furniture.Company = company;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
