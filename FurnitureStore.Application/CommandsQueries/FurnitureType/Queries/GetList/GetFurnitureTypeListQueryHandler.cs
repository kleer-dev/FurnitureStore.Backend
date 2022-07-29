using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FurnitureStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.GetList;

public class GetFurnitureTypeListQueryHandler
    : IRequestHandler<GetFurnitureTypeListQuery, GetFurnitureTypeListVm>
{
    private readonly IFurnitureStoreDbContext _dbContext;

    public GetFurnitureTypeListQueryHandler(IFurnitureStoreDbContext dbContext, 
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private readonly IMapper _mapper;

    public async Task<GetFurnitureTypeListVm> Handle(GetFurnitureTypeListQuery request, 
        CancellationToken cancellationToken)
    {
        var furnitureTypes = await _dbContext.FurnitureTypes
            .ProjectTo<FurnitureTypeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetFurnitureTypeListVm() { FurnitureTypes = furnitureTypes };
    }
}
