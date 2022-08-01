using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Commands.Delete;

public class DeleteFurnitureCommandHandler : IRequestHandler<DeleteFurnitureCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly ICacheManager<Domain.Furniture> _cacheManager;

    public DeleteFurnitureCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.Furniture> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(DeleteFurnitureCommand request, 
        CancellationToken cancellationToken)
    {
        var furniture = await _dbContext.Furnitures
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.Id);

        _dbContext.Furnitures.Remove(furniture);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.RemoveCacheValue(request.Id);

        return Unit.Value;
    }
}
