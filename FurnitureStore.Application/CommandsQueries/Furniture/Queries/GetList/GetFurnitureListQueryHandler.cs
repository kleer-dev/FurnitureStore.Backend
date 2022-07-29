using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.GetList;

public class GetFurnitureListQueryHandler
    : IRequestHandler<GetFurnitureListQuery, GetFurnitureListVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetFurnitureListQueryHandler(IFurnitureStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetFurnitureListVm> Handle(GetFurnitureListQuery request, 
        CancellationToken cancellationToken)
    {
        var furnitures = await _dbContext.Furnitures
            .ProjectTo<FurnitureDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetFurnitureListVm() { Furnitures = furnitures };
    }
}
