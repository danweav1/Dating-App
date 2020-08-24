using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                // create some roles
                var roles = new List<Role>
                {
                    new Role{Name = "Member"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "VIP"}
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    user.Photos.SingleOrDefault().IsApproved = true;
                    await userManager.CreateAsync(user, "password");
                    await userManager.AddToRoleAsync(user, "Member");
                }

                // create admin user
                var adminUser = new User
                {
                    UserName = "Admin"
                };

                var result = await userManager.CreateAsync(adminUser, "password");

                if (result.Succeeded)
                {
                    var admin = await userManager.FindByNameAsync("Admin");
                    await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
                }
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}