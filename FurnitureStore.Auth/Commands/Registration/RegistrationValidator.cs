using FluentValidation;

namespace FurnitureStore.Auth.Commands.Registration;

public class RegistrationValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(q => q.PhoneNumber)
            .NotEmpty()
            .MaximumLength(13)
            .MinimumLength(13);

        RuleFor(q => q.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(q => q.Password)
            .MinimumLength(8)
            .NotEmpty();
    }
}
