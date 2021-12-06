using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                Price = 899,
                Description = "Wydajna karta graficzna o niskiej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\69fb19b4-9ee7-4a36-9bb4-b3679a09bb38.png"
                },
                new Product()
                {
                Name = "GTX 1050Ti",
                Price = 1200,
                Description = "Wydajna karta graficzna o średniej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\69fb19b4-9ee7-4a36-9bb4-b3679a09bb38.png"
                },
                new Product()
                {
                Name = "RTX 2070",
                Price = 2600,
                Description = "Wydajna karta graficzna o średniej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\69fb19b4-9ee7-4a36-9bb4-b3679a09bb38.png"
                },
                new Product(){
                Name = "RTX 3060Ti",
                Price = 4300,
                Description = "Wydajna karta graficzna o wysokiej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\69fb19b4-9ee7-4a36-9bb4-b3679a09bb38.png"
                },
                new Product(){
                Name = "RTX 3090",
                Price = 15900,
                Description = "Wydajna karta graficzna o potężnej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\69fb19b4-9ee7-4a36-9bb4-b3679a09bb38.png"
                },
                new Product()
                {
                Name = "Samsung SSD",
                Price = 299,
                Description = "dysk ssd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e47b6ce5-d165-4f54-a3d5-28a5c0b077eb.png"
                },
                new Product()
                {
                Name = "Intel i7",
                Price = 999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\3811d45a-2469-4910-9501-a9125dd84b5b.png"
                },
                new Product()
                {
                Name = "Asus b560",
                Price = 599,
                Description = "płyta główna",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\82bde16e-192a-4b57-abca-fc90e90e83b2.png"
                },
                new Product()
                {
                Name = "Corsair VS450",
                Price = 399,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e579887b-b248-4445-be82-bcb36f19d2c5.png"
                },
                new Product()
                {
                Name = "SilentiumPb ds13",
                Price = 199,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\87f8f519-e743-44b9-ba91-6a4a0a51644f.png"
                },
                new Product()
                {
                Name = "Gen-Z 16GB",
                Price = 299,
                Description = "Pamięć RAM",
                Quantity = 10,
                SubCategoryId = 5,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\446d23d0-af6d-4f58-b392-e99f27d78867.png"
                },
                new Product()
                {
                Name = "Corsair CoolBud 2",
                Price = 299,
                Description = "chłodzenie",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\fc351910-a922-41d4-8453-aa898520b1d5.png"
                },
                new Product()
                {
                Name = "Intel i3",
                Price = 499,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\3811d45a-2469-4910-9501-a9125dd84b5b.png"
                },
                new Product()
                {
                Name = "Intel i5",
                Price = 999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\3811d45a-2469-4910-9501-a9125dd84b5b.png"
                },
                new Product()
                {
                Name = "Intel i9",
                Price = 4999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\3811d45a-2469-4910-9501-a9125dd84b5b.png"
                },
                new Product()
                {
                Name = "be quiet! 400W",
                Price = 299,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e579887b-b248-4445-be82-bcb36f19d2c5.png"
                },
                new Product()
                {
                Name = "be quiet! 500W",
                Price = 350,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e579887b-b248-4445-be82-bcb36f19d2c5.png"
                },
                new Product()
                {
                Name = "be quiet! 650W",
                Price = 399,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e579887b-b248-4445-be82-bcb36f19d2c5.png"
                },
                new Product()
                {
                Name = "be quiet! 850W",
                Price = 499,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e579887b-b248-4445-be82-bcb36f19d2c5.png"
                },
                new Product()
                {
                Name = "Seagate BARRACUDA",
                Price = 299,
                Description = "dysk hdd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e47b6ce5-d165-4f54-a3d5-28a5c0b077eb.png"
                },
                new Product(){
                Name = "Gigabyte M.2 PCIe",
                Price = 269,
                Description = "dysk ssd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\e47b6ce5-d165-4f54-a3d5-28a5c0b077eb.png"
                },
                new Product()
                {
                Name = "Corsair Airflow",
                Price = 459,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\87f8f519-e743-44b9-ba91-6a4a0a51644f.png"
                },
                new Product()
                {
                Name = "KRUX Naos",
                Price = 299,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\87f8f519-e743-44b9-ba91-6a4a0a51644f.png"
                },
                new Product()
                {
                Name = "SilentiumPC Fera 5",
                Price = 125,
                Description = "chłodzenie",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\fc351910-a922-41d4-8453-aa898520b1d5.png"
                },
                new Product()
                {
                Name = "GOODRAM 8GB",
                Price = 139,
                Description = "Pamięć RAM",
                Quantity = 10,
                SubCategoryId = 5,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\446d23d0-af6d-4f58-b392-e99f27d78867.png"
                },
                new Product()
                {
                Name = "MSI B550",
                Price = 599,
                Description = "płyta główna",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\82bde16e-192a-4b57-abca-fc90e90e83b2.png"
                },
                new Product()
                {
                Name = "MSI B550M",
                Price = 399,
                Description = "płyta główna",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\82bde16e-192a-4b57-abca-fc90e90e83b2.png"
                },
            };

            products.ForEach(c => context.Products.Add(c));
            context.SaveChanges();

            var details = new List<DetailedDataProduct>()
            {
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Corsair CoolBud 2").FirstOrDefault().ProductId, Name = "Length", Value = "280" , Product = products.Where(x => x.Name == "Corsair CoolBud 2").FirstOrDefault()}, // COOLING

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Gen-Z 16GB").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "3200" , Product = products.Where(x => x.Name == "Gen-Z 16GB").FirstOrDefault()}, //RAM
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Gen-Z 16GB").FirstOrDefault().ProductId, Name = "RamStandard", Value = "DDR4" , Product = products.Where(x => x.Name == "Gen-Z 16GB").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault().ProductId, Name = "MaxGpuLenght", Value = "360", Product = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault() }, // CASE
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault().ProductId, Name = "MaxCoolingHeight", Value = "170", Product = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "ATX" , Product = products.Where(x => x.Name == "SilentiumPb ds13").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Corsair VS450").FirstOrDefault().ProductId, Name = "MaxPower", Value = "450", Product = products.Where(x => x.Name == "Corsair VS450").FirstOrDefault() }, // PSU

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Asus b560").FirstOrDefault().ProductId, Name = "SATA", Value = "3" , Product = products.Where(x => x.Name == "Asus b560").FirstOrDefault() }, //MOTHERBOARD
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Asus b560").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "ATX", Product = products.Where(x => x.Name == "Asus b560").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Asus b560").FirstOrDefault().ProductId, Name = "RamStandard", Value = "DDR4", Product = products.Where(x => x.Name == "Asus b560").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Asus b560").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "3200;2600;2200;2400", Product = products.Where(x => x.Name == "Asus b560").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Asus b560").FirstOrDefault().ProductId, Name = "SupportedCpuFamilies", Value = "i3;i5;i7;i9", Product = products.Where(x => x.Name == "Asus b560").FirstOrDefault() },

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i7").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "2.5" , Product = products.Where(x => x.Name == "Intel i7").FirstOrDefault()}, //CPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i7").FirstOrDefault().ProductId, Name = "Efficiency", Value = "3", Product = products.Where(x => x.Name == "Intel i7").FirstOrDefault() }, // 1 - 5
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i7").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "125" , Product = products.Where(x => x.Name == "Intel i7").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i7").FirstOrDefault().ProductId, Name = "CpuFamily", Value = "i7" , Product = products.Where(x => x.Name == "Intel i7").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Samsung SSD").FirstOrDefault().ProductId, Name = "SATA", Value = "3", Product = products.Where(x => x.Name == "Samsung SSD").FirstOrDefault() }, //MEMORY

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 960").FirstOrDefault().ProductId, Name = "Length", Value = "227" , Product = products.Where(x => x.Name == "GTX 960").FirstOrDefault()}, //GPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 960").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "450" , Product = products.Where(x => x.Name == "GTX 960").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 960").FirstOrDefault().ProductId, Name = "Efficiency", Value = "1" , Product = products.Where(x => x.Name == "GTX 960").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault().ProductId, Name = "Length", Value = "203" , Product = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault()}, //GPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "550" , Product = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault().ProductId, Name = "Efficiency", Value = "2" , Product = products.Where(x => x.Name == "GTX 1050Ti").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 2070").FirstOrDefault().ProductId, Name = "Length", Value = "220" , Product = products.Where(x => x.Name == "RTX 2070").FirstOrDefault()}, //GPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 2070").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "600" , Product = products.Where(x => x.Name == "RTX 2070").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 2070").FirstOrDefault().ProductId, Name = "Efficiency", Value = "3" , Product = products.Where(x => x.Name == "RTX 2070").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault().ProductId, Name = "Length", Value = "232" , Product = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault()}, //GPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "650" , Product = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault().ProductId, Name = "Efficiency", Value = "4" , Product = products.Where(x => x.Name == "RTX 3060Ti").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3090").FirstOrDefault().ProductId, Name = "Length", Value = "294" , Product = products.Where(x => x.Name == "RTX 3090").FirstOrDefault()}, //GPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3090").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "850" , Product = products.Where(x => x.Name == "RTX 3090").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "RTX 3090").FirstOrDefault().ProductId, Name = "Efficiency", Value = "5" , Product = products.Where(x => x.Name == "RTX 3090").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i3").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "3.7" , Product = products.Where(x => x.Name == "Intel i3").FirstOrDefault()}, //CPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i3").FirstOrDefault().ProductId, Name = "Efficiency", Value = "1", Product = products.Where(x => x.Name == "Intel i3").FirstOrDefault() }, // 1 - 5
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i3").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "200" , Product = products.Where(x => x.Name == "Intel i3").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i3").FirstOrDefault().ProductId, Name = "CpuFamily", Value = "i3" , Product = products.Where(x => x.Name == "Intel i3").FirstOrDefault()},

                new DetailedDataProduct { ProductId =  products.Where(x => x.Name == "Intel i5").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "4.6" , Product = products.Where(x => x.Name == "Intel i5").FirstOrDefault()}, //CPU
                new DetailedDataProduct { ProductId =  products.Where(x => x.Name == "Intel i5").FirstOrDefault().ProductId, Name = "Efficiency", Value = "2", Product =  products.Where(x => x.Name == "Intel i5").FirstOrDefault()}, // 1 - 5
                new DetailedDataProduct { ProductId =  products.Where(x => x.Name == "Intel i5").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "400" , Product =  products.Where(x => x.Name == "Intel i5").FirstOrDefault()},
                new DetailedDataProduct { ProductId =  products.Where(x => x.Name == "Intel i5").FirstOrDefault().ProductId, Name = "CpuFamily", Value = "i5" , Product =  products.Where(x => x.Name == "Intel i5").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i9").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "5.3" , Product = products.Where(x => x.Name == "Intel i9").FirstOrDefault()}, //CPU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i9").FirstOrDefault().ProductId, Name = "Efficiency", Value = "5", Product = products.Where(x => x.Name == "Intel i9").FirstOrDefault() }, // 1 - 5
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i9").FirstOrDefault().ProductId, Name = "PowerNeeded", Value = "500" , Product = products.Where(x => x.Name == "Intel i9").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Intel i9").FirstOrDefault().ProductId, Name = "CpuFamily", Value = "i9" , Product = products.Where(x => x.Name == "Intel i9").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "be quiet! 400W").FirstOrDefault().ProductId, Name = "MaxPower", Value = "400", Product = products.Where(x => x.Name == "be quiet! 400W").FirstOrDefault() }, // PSU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "be quiet! 500W").FirstOrDefault().ProductId, Name = "MaxPower", Value = "500", Product = products.Where(x => x.Name == "be quiet! 500W").FirstOrDefault() }, // PSU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "be quiet! 650W").FirstOrDefault().ProductId, Name = "MaxPower", Value = "650", Product = products.Where(x => x.Name == "be quiet! 650W").FirstOrDefault() }, // PSU
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "be quiet! 850W").FirstOrDefault().ProductId, Name = "MaxPower", Value = "850", Product = products.Where(x => x.Name == "be quiet! 850W").FirstOrDefault() }, // PSU

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Seagate BARRACUDA").FirstOrDefault().ProductId, Name = "SATA", Value = "3", Product = products.Where(x => x.Name == "Seagate BARRACUDA").FirstOrDefault() }, //MEMORY
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Gigabyte M.2 PCIe").FirstOrDefault().ProductId, Name = "M.2 PCIe", Value = "3", Product = products.Where(x => x.Name == "Gigabyte M.2 PCIe").FirstOrDefault() }, //MEMORY

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault().ProductId, Name = "MaxGpuLenght", Value = "300", Product = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault() }, // CASE
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault().ProductId, Name = "MaxCoolingHeight", Value = "140", Product = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "ATX" , Product = products.Where(x => x.Name == "Corsair Airflow").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault().ProductId, Name = "MaxGpuLenght", Value = "280", Product = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault() }, // CASE
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault().ProductId, Name = "MaxCoolingHeight", Value = "120", Product = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "microATX" , Product = products.Where(x => x.Name == "KRUX Naos").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "SilentiumPC Fera 5").FirstOrDefault().ProductId, Name = "Length", Value = "155" , Product = products.Where(x => x.Name == "SilentiumPC Fera 5").FirstOrDefault()}, // COOLING

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GOODRAM 8GB").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "1600" , Product = products.Where(x => x.Name == "GOODRAM 8GB").FirstOrDefault()}, //RAM
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "GOODRAM 8GB").FirstOrDefault().ProductId, Name = "RamStandard", Value = "DDR3" , Product = products.Where(x => x.Name == "GOODRAM 8GB").FirstOrDefault()},

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550").FirstOrDefault().ProductId, Name = "M.2 PCIe", Value = "3" , Product = products.Where(x => x.Name == "MSI B550").FirstOrDefault() }, //MOTHERBOARD
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "ATX", Product = products.Where(x => x.Name == "MSI B550").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550").FirstOrDefault().ProductId, Name = "RamStandard", Value = "DDR4", Product = products.Where(x => x.Name == "MSI B550").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "3200;2600;2200;2400", Product = products.Where(x => x.Name == "MSI B550").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550").FirstOrDefault().ProductId, Name = "SupportedCpuFamilies", Value = "i7;i9", Product = products.Where(x => x.Name == "MSI B550").FirstOrDefault() },

                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550M").FirstOrDefault().ProductId, Name = "SATA", Value = "3" , Product = products.Where(x => x.Name == "MSI B550M").FirstOrDefault() }, //MOTHERBOARD
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550M").FirstOrDefault().ProductId, Name = "MotherBoardStandard", Value = "microATX", Product = products.Where(x => x.Name == "MSI B550M").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550M").FirstOrDefault().ProductId, Name = "RamStandard", Value = "DDR4", Product = products.Where(x => x.Name == "MSI B550M").FirstOrDefault()},
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550M").FirstOrDefault().ProductId, Name = "ClockSpeed", Value = "3200;2600", Product = products.Where(x => x.Name == "MSI B550M").FirstOrDefault() },
                new DetailedDataProduct { ProductId = products.Where(x => x.Name == "MSI B550M").FirstOrDefault().ProductId, Name = "SupportedCpuFamilies", Value = "i3;i5;i7", Product = products.Where(x => x.Name == "MSI B550M").FirstOrDefault() },
            };

            details.ForEach(s => context.DetailedDataProducts.Add(s));
            context.SaveChanges();
        }
    }
}