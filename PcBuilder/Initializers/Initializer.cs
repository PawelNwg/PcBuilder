using Microsoft.AspNetCore.Identity;
using PcBuilder.Data;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Initializers
{
    public static class Initializer
    {
        public static async Task InitializeData(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Database.EnsureCreated())
            {
                return;
            }
            

            var role = new IdentityRole();
            role.Name = "Admin";
            await roleManager.CreateAsync(role);

            var user = new AppUser()
            {
                Email = "admin@admin.pl",
                UserName = "admin@admin.pl",
                Name = "Admin",
                Surname = "Admin",
                EmailConfirmed = true
            };

            string pass = "Admin1234%";

            IdentityResult checkUser = await userManager.CreateAsync(user, pass);

            if (checkUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            var secondUser = new AppUser()
            {
                Email = "jan@kowalski.pl",
                UserName = "jan@kowalski.pl",
                Name = "Jan",
                Surname = "Kowalski",
                EmailConfirmed = true
            };

            pass = "Haslo1234%";

            await userManager.CreateAsync(secondUser, pass);

            var categories = new List<Category>()
            {
                new Category(){Name = "Pamięć" },
                new Category(){Name = "GPU" },
                new Category(){Name = "Obudowy" },
                new Category(){Name = "RAM" },
                new Category(){Name = "Płyty główne" },
                new Category(){Name = "CPU" },
                new Category(){Name = "Zasialcze" },
                new Category(){Name = "Chłodzenie" },
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var sub = new Subcategory() {Name = "legacy gpu", CategoryId = 2 };
            context.Subcategories.Add(sub);
            context.SaveChanges();

            var gpu = new List<Product>()
            {
                new Product()
                {
                Name = "GTX 960",
                Price = 499,
                Description = "Wydajna karta graficzna o ogromnej mocy",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\568d7ce1-deba-4341-8a9b-08152333924e.jpeg"
                },
                new Product()
                {
                Name = "GTX 961",
                Price = 299,
                Description = "Wydajna karta graficzna o ogromnej mocy",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\568d7ce1-deba-4341-8a9b-08152333924e.jpeg"
                }
            };

            gpu.ForEach(c => context.Products.Add(c));
            context.SaveChanges();
        }
    }
}