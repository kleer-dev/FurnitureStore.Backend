using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Company.Queries.Get;

public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
{
    public GetCompanyQueryValidator()
    {
        RuleFor(f => f.Id).NotEmpty();
    }
}
