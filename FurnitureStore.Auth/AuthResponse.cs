using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Domain;

namespace FurnitureStore.Auth;

public class AuthResponse
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
