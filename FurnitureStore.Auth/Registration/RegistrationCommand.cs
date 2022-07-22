using MediatR;

namespace FurnitureStore.Auth.Registration;

public class RegistrationCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
