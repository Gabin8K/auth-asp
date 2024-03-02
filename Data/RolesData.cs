using Microsoft.AspNetCore.Identity;

namespace CustomAuth.Data;

public abstract class RolesData
{
    private static readonly string[] Roles = ["Admin", "Customer", "User"];

    public static async void InitializeRequest(IServiceProvider serviceProvider)
    {
        using var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        if (roleManager == null)
        {
            return;
        }

        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role
                });
            }
        }
    }
}