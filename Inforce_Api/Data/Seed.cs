using Inforce_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static Inforce_Api.Utility.SD;

namespace Inforce_Api.Data
{
    public static class Seed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserDataSeed.json");

            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData, options) ?? new List<ApplicationUser>();

            IEnumerable<UserRoles> enumRoles = (IEnumerable<UserRoles>)Enum.GetValues(typeof(UserRoles));

            var roles = new List<IdentityRole<int>>(enumRoles.Select(role => new IdentityRole<int>(role.ToString())));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.ADMIN));
            }

        }
    }
}
