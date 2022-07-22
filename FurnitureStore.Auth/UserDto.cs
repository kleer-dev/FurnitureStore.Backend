namespace FurnitureStore.Auth;

public class UserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public string Token { get; set; }
}
