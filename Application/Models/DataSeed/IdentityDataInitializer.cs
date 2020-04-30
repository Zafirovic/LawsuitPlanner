using Application.Models;
using Microsoft.AspNetCore.Identity;

public static class IdentityDataInitializer
{
    public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
            SeedRoles(roleManager);
            SeedUsers(userManager);
    }

    public static void SeedUsers(UserManager<User> userManager)
    {
        if (userManager.FindByNameAsync("user1").Result == null)
        {
            User user = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                name = "Administrator",
                surname = "Administrator"
            };

            var result = userManager.CreateAsync(user, "Admin@123").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "admin").Wait();
            }
        }
    }

    public static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("korisnik").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "korisnik";
            role.NormalizedName = "KORISNIK";
            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }

        if (!roleManager.RoleExistsAsync("admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "admin";
            role.NormalizedName = "ADMIN";
            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }
    }
}