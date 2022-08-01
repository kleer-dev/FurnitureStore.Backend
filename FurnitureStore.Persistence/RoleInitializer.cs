using FurnitureStore.Auth;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Persistence;

public class RoleInitializer
{
    public static async Task InitializerAsync(UserManager<User> userManager, 
        RoleManager<IdentityRole<long>> roleManager, 
        IJwtGenerator jwtGenerator)
    {
        if (await roleManager.FindByNameAsync("Admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole<long>("Admin"));
        }
        if (await userManager.FindByNameAsync("Admin") == null)
        {
            var refreshToken = jwtGenerator.GenerateRefreshToken();
        
            var admin = new User
            {
                UserName = "Admin",
                Email = "adminadmin@gmail.com",
                PhoneNumber = "+37529844123",
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            var result = await userManager.CreateAsync(admin, "adminpassword");
            
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}