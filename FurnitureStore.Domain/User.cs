using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Domain;

public class User : IdentityUser<long>
{
    public decimal Balance { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }   

    public List<Order> Orders { get; set; }
}
