using AutoMapper;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class GetFurnitureTypeQueryHandler : IRequestHandler<GetFurnitureTypeQuery, FurnitureTypeVm>
{
    private readonly IFurnitureStoreDbContext _dbcontext;
    private readonly IMapper _mapper;

    public GetFurnitureTypeQueryHandler(IFurnitureStoreDbContext dbcontext, 
        IMapper mapper)
    {
        _dbcontext = dbcontext;
        _mapper = mapper;
    }

    public async Task<FurnitureTypeVm> Handle(GetFurnitureTypeQuery request,
        CancellationToken cancellationToken)
    {
        var furnitureType = await _dbcontext.FurnitureTypes
            .Include(f => f.Furnitures)
                .ThenInclude(f => f.Company)
            .FirstOrDefaultAsync(fType => fType.Id == request.Id, cancellationToken);

        if (furnitureType == null)
            throw new NotFoundException(nameof(Domain.FurnitureType), request.Id);

        return _mapper.Map<FurnitureTypeVm>(furnitureType);
    }
}
