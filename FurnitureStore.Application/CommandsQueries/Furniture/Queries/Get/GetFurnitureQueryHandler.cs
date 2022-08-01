using AutoMapper;
using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;

public class GetFurnitureQueryHandler : IRequestHandler<GetFurnitureQuery, FurnitureVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Furniture> _cacheManager;

    public GetFurnitureQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper, 
        ICacheManager<Domain.Furniture> cacheManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<FurnitureVm> Handle(GetFurnitureQuery request, 
        CancellationToken cancellationToken)
    {
        var furnitureQuery = async () => await _dbContext.Furnitures
            .Include(f => f.FurnitureType)
            .Include(f => f.Company)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        
        var furniture = await _cacheManager
            .GetOrSetCacheValue(request.Id, furnitureQuery);
        
        furniture.FurnitureType.Furnitures = null!;
        furniture.Company.Furnitures = null!;

        return _mapper.Map<FurnitureVm>(furniture);
    }
}
