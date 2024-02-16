using Bookstore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(BookstoreContext context, UserManager<Account> userManager, RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();

            // Sprawdzenie, czy role istnieją
            if (!context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role{Name = "Admin"},
                    new Role{Name = "User"}
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // Sprawdzenie, czy użytkownicy istnieją
            if (!context.Users.Any())
            {
                var adminUser = new Account { UserName = "admin@example.com", Email = "admin@example.com" };
                await userManager.CreateAsync(adminUser, "AdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");

                var regularUser = new Account { UserName = "user@example.com", Email = "user@example.com" };
                await userManager.CreateAsync(regularUser, "UserPassword123!");
                await userManager.AddToRoleAsync(regularUser, "User");
            }
        }
    }
}
