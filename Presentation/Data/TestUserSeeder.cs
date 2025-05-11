using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Data;

public static class TestUserSeeder
{
    public static async Task SeedTestUserAsync(IServiceProvider rootProvider)
    {
        using var scope = rootProvider.CreateScope(); // ✅ create a scoped service provider
        var scopedProvider = scope.ServiceProvider;

        var userManager = scopedProvider.GetRequiredService<UserManager<AppUserEntity>>();

        string testEmail = "testuser@example.com";
        string password = "Test1234!";

        var existingUser = await userManager.FindByEmailAsync(testEmail);
        if (existingUser == null)
        {
            var user = new AppUserEntity
            {
                UserName = testEmail,
                Email = testEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                Console.WriteLine("✅ Test user created.");
            }
            else
            {
                foreach (var error in result.Errors)
                    Console.WriteLine($"❌ Error: {error.Description}");
            }
        }
    }
}

