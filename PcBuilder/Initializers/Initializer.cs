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
                new Category(){Name = "Zasilacze" },
                new Category(){Name = "Chłodzenie" },
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var sub = new List<Subcategory>()
            {
                new Subcategory(){Name = "samsung ssd", CategoryId = 1 },
                new Subcategory(){Name = "legacy gpu", CategoryId = 2 },
                new Subcategory(){Name = "cheap case", CategoryId = 3 },
                new Subcategory(){Name = "led ram", CategoryId = 4 },
                new Subcategory(){Name = "gaming motherboard", CategoryId = 5 },
                new Subcategory(){Name = "intel cpu", CategoryId = 6 },
                new Subcategory(){Name = "corsair power supply", CategoryId = 7 },
                new Subcategory(){Name = "corsair cooling", CategoryId = 8 },
            };

            sub.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            var products = new List<Product>()
            {
                new Product()
                {
                Name = "GTX 960",
                Price = 499,
                Description = "Wydajna karta graficzna o ogromnej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\568d7ce1-deba-4341-8a9b-08152333924e.jpeg"
                },
                new Product()
                {
                Name = "Samsung SSD",
                Price = 299,
                Description = "dysk ssd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\1cf1494f-f007-48dd-93c4-61ad8a99e8f4.png"
                },
                new Product()
                {
                Name = "Intel i7",
                Price = 999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\7c533eea-80de-4fe7-a162-72097c675259.jpg"
                },
                new Product()
                {
                Name = "Płyta główna asus b560",
                Price = 599,
                Description = "motherboard",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\7ba6432d-ca42-49dd-8d9e-240b85c9004c.png"
                },
                new Product()
                {
                Name = "Corsair VS450",
                Price = 399,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\8f2cca3c-8c49-4164-8d65-5efa1f771d35.png"
                },
                new Product()
                {
                Name = "SilentiumPb ds13",
                Price = 199,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\4833ea6e-7a50-4045-bcd5-9095ba5058ac.png"
                },
                new Product()
                {
                Name = "Gen-Z 16GB",
                Price = 299,
                Description = "Pamięć RAM",
                Quantity = 10,
                SubCategoryId = 5,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\c2dc28f0-9545-4828-b530-f36478fb1bfe.jpg"
                },
                new Product()
                {
                Name = "Corsair CoolBud 2",
                Price = 299,
                Description = "chłodzenie",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\391b6b0e-2fb1-4d6a-8354-63e68498cd2d.png"
                },
            };

            products.ForEach(c => context.Products.Add(c));
            context.SaveChanges();
        }
    }
}