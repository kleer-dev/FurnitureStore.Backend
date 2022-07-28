﻿using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.Get;

public class GetCompanyQuery : IRequest<CompanyVm>
{
    public long Id { get; set; }
}
