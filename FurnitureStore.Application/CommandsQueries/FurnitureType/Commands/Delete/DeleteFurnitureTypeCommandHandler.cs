using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Delete;

public class DeleteFurnitureTypeCommandHandler : IRequestHandler<DeleteFurnitureTypeCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public DeleteFurnitureTypeCommandHandler(IFurnitureStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteFurnitureTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var furnitureType = await _dbContext.FurnitureTypes
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (furnitureType == null)
            throw new NotFoundException(nameof(Domain.FurnitureType), request.Id);

        _dbContext.FurnitureTypes.Remove(furnitureType);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
