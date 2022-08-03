using MediatR;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Delete;

public class DeleteCompanyCommand : IRequest
{
    public long Id { get; set; }
}
