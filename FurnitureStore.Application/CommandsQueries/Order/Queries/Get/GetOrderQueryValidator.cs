using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Order.Queries.Get;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
    }
}
