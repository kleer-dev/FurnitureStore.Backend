using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Auth.Exceptions;

public class UserRegistrationException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; private set; }

    public UserRegistrationException(string message) : base(message)
    {
        Errors = new List<IdentityError>();
    }

    public UserRegistrationException(IList<IdentityError> errors)
        : base($"{CreateErrorMessage(errors)}")
    {
        Errors = errors;
    }

    public UserRegistrationException(string message, IList<IdentityError> errors)
        : base($"{message} {CreateErrorMessage(errors)}")
    {
        Errors = errors;
    }

    private static string CreateErrorMessage(IEnumerable<IdentityError> errors)
    {
        var arr = errors.Select(x => $"{Environment.NewLine} - {x}: {x}");

        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}
