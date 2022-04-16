using System.Security.Cryptography;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeed.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            
            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Test@123");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Test@123");
            await userManager.AddToRolesAsync(admin, new[] {"Admin"});
            
            /*
            if (users == null) return;
            int i = 1;
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                await context.Users.AddAsync(user);
                i++;
            }

            await context.SaveChangesAsync();
            */
        }

        public static async Task SeedBooks(DataContext context)
        {
            if (await context.Books.AnyAsync()) return;

            var bookData = await System.IO.File.ReadAllTextAsync("Data/BookSeed.json");
            var books = JsonSerializer.Deserialize<List<Book>>(bookData);
            if (books == null) return;
            int i = 1;
            foreach (var book in books)
            {

                using var hmac = new HMACSHA512();
                await context.Books.AddAsync(book);
                i++;
            }

            await context.SaveChangesAsync();
        }
    }
}