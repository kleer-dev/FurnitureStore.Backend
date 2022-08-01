using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Commands.Update;

public class UpdateFurnitureTypeCommandHandler : IRequestHandler<UpdateFurnitureTypeCommand, Unit>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly ICacheManager<Domain.FurnitureType> _cacheManager;

    public UpdateFurnitureTypeCommandHandler(IFurnitureStoreDbContext dbContext, 
        ICacheManager<Domain.FurnitureType> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateFurnitureTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var isFurnitureTypeExist = await _dbContext.FurnitureTypes
               .AnyAsync(c => c.Name == request.Name && c.Id != request.Id, cancellationToken);

        if (isFurnitureTypeExist)
            throw new RecordIsExistException(request.Name);

        var furnitureType = await _dbContext.FurnitureTypes
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (furnitureType == null)
            throw new NotFoundException(nameof(Domain.FurnitureType), request.Id);

        furnitureType.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, furnitureType);

        return Unit.Value;
    }
}
