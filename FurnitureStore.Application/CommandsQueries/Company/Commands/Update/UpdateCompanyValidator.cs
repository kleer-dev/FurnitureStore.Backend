using FluentValidation;

namespace FurnitureStore.Application.CommandsQueries.Company.Commands.Update;

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(150);
    }
}
