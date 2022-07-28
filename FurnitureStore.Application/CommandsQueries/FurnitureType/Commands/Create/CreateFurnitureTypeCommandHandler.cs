using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Create;

public class CreateFurnitureTypeCommandHandler : IRequestHandler<CreateFurnitureTypeCommand, long>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public CreateFurnitureTypeCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(CreateFurnitureTypeCommand request,
        CancellationToken cancellationToken)
    {
        var isTypeExist = await _dbContext.FurnitureTypes
            .AnyAsync(c => c.Name == request.Name, cancellationToken);

        if (isTypeExist)
            throw new RecordIsExistException(request.Name);

        var furnitureType = new Domain.FurnitureType
        {
            Name = request.Name
        };

        await _dbContext.FurnitureTypes.AddAsync(furnitureType, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return furnitureType.Id;
    }
}
