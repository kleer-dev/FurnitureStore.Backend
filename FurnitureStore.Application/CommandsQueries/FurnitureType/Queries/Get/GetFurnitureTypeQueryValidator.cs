using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.FurnitureType.Queries.Get;

public class GetFurnitureTypeQueryValidator : AbstractValidator<GetFurnitureTypeQuery>
{
    public GetFurnitureTypeQueryValidator()
    {
        RuleFor(f => f.Id).NotEmpty();
    }
}
