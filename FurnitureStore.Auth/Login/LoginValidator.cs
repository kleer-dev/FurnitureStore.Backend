using FluentValidation;

namespace FurnitureStore.Auth.Login;

public class LoginValidator : AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(q => q.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(q => q.Password)
            .MinimumLength(8)
            .NotEmpty();
    }
}
