using AutoMapper;
using FurnitureStore.Application.Common.Exceptions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;

public class GetFurnitureQueryHandler : IRequestHandler<GetFurnitureQuery, FurnitureVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetFurnitureQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<FurnitureVm> Handle(GetFurnitureQuery request, 
        CancellationToken cancellationToken)
    {
        var furniture = await _dbContext.Furnitures
            .Include(f => f.FurnitureType)
            .Include(f => f.Company)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (furniture == null)
            throw new NotFoundException(nameof(Domain.Furniture), request.Id);

        furniture.FurnitureType.Furnitures = null!;
        furniture.Company.Furnitures = null!;

        return _mapper.Map<FurnitureVm>(furniture);
    }
}
