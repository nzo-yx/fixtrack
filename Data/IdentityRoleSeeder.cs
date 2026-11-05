using Microsoft.AspNetCore.Identity;

namespace FixTrack.Web.Data;

public static class IdentityRoleSeeder
{
    private static readonly string[] RoleNames =
    [
        "Resident",
        "Official",
        "Crew",
        "Admin"
    ];

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var roleName in RoleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Could not create role '{roleName}': {errors}");
            }
        }
    }
}
