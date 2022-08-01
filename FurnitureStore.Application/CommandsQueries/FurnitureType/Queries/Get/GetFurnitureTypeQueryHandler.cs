using AutoMapper;
using FurnitureStore.Application.Common.Cache;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class GetFurnitureTypeQueryHandler : IRequestHandler<GetFurnitureTypeQuery, FurnitureTypeVm>
{
    private readonly IFurnitureStoreDbContext _dbcontext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.FurnitureType> _cacheManager;

    public GetFurnitureTypeQueryHandler(IFurnitureStoreDbContext dbcontext, 
        IMapper mapper, ICacheManager<Domain.FurnitureType> cacheManager)
    {
        _dbcontext = dbcontext;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<FurnitureTypeVm> Handle(GetFurnitureTypeQuery request,
        CancellationToken cancellationToken)
    {
        var furnitureTypeQuery = async () => await _dbcontext.FurnitureTypes
            .Include(f => f.Furnitures)
                .ThenInclude(f => f.Company)
            .FirstOrDefaultAsync(fType => fType.Id == request.Id, cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var furnitureType = await _cacheManager.GetOrSetCacheValue(request.Id, furnitureTypeQuery);

        return _mapper.Map<FurnitureTypeVm>(furnitureType);
    }
}
