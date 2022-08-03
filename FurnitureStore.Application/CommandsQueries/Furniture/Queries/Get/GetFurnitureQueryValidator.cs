using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Furniture.Queries.Get;

public class GetFurnitureQueryValidator : AbstractValidator<GetFurnitureQuery>
{
    public GetFurnitureQueryValidator()
    {
        RuleFor(f => f.Id).NotEmpty();
    }
}
