using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class GetFurnitureTypeValidator : AbstractValidator<GetFurnitureTypeQuery>
{
    public GetFurnitureTypeValidator()
    {
        RuleFor(f => f.Id).NotEmpty();
    }
}
