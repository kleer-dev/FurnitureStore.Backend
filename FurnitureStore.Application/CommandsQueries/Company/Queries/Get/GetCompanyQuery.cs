using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.Get;

public class GetCompanyQuery : IRequest<CompanyVm>
{
    public long Id { get; set; }
}
