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
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\63836162-3717-4015-b762-13de00d8b06e.png"
                },
                new Product()
                {
                Name = "GTX 1050Ti",
                Price = 1200,
                Description = "Wydajna karta graficzna o średniej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\c4ab17da-c1b5-4c56-b3bf-de1f1a4986a8.png"
                },
                new Product()
                {
                Name = "RTX 2070",
                Price = 2600,
                Description = "Wydajna karta graficzna o średniej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\62442976-61b6-4a28-b164-7d659a404cbd.jpg"
                },
                new Product(){
                Name = "RTX 3060Ti",
                Price = 4300,
                Description = "Wydajna karta graficzna o wysokiej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\27d1f4b7-28ad-485c-875d-ea790872f59e.png"
                },
                new Product(){
                Name = "RTX 3090",
                Price = 15900,
                Description = "Wydajna karta graficzna o potężnej mocy",
                Quantity = 10,
                SubCategoryId = 7,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\6c97c9f1-52e9-4cbf-b733-a2436b3bce7e.png"
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
                Name = "Asus b560",
                Price = 599,
                Description = "płyta główna",
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
                new Product()
                {
                Name = "Intel i3",
                Price = 499,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\5c16b72a-442d-4d35-bf1c-d3e414528943.png"
                },
                new Product()
                {
                Name = "Intel i5",
                Price = 999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\584636cf-e6f2-4cb4-a036-76080540479f.png"
                },
                new Product()
                {
                Name = "Intel i9",
                Price = 4999,
                Description = "cpu",
                Quantity = 10,
                SubCategoryId = 3,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\442f1137-274e-43c3-8574-90f3ae33de92.jpg"
                },
                new Product()
                {
                Name = "be quiet! 400W",
                Price = 299,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\263706a8-e569-4829-af41-b3bb369f4710.jpg"
                },
                new Product()
                {
                Name = "be quiet! 500W",
                Price = 350,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\263706a8-e569-4829-af41-b3bb369f4710.jpg"
                },
                new Product()
                {
                Name = "be quiet! 650W",
                Price = 399,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\263706a8-e569-4829-af41-b3bb369f4710.jpg"
                },
                new Product()
                {
                Name = "be quiet! 850W",
                Price = 499,
                Description = "zasilacz komputerowy",
                Quantity = 10,
                SubCategoryId = 2,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\263706a8-e569-4829-af41-b3bb369f4710.jpg"
                },
                new Product()
                {
                Name = "Seagate BARRACUDA",
                Price = 299,
                Description = "dysk hdd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\7a992821-df49-483e-aacb-805609eb478e.jpg"
                },
                new Product(){
                Name = "Gigabyte M.2 PCIe",
                Price = 269,
                Description = "dysk ssd",
                Quantity = 10,
                SubCategoryId = 8,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\85e999cb-a179-4a80-abfa-c3520615bf15.jpg"
                },
                new Product()
                {
                Name = "Corsair Airflow",
                Price = 459,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\5a687a75-bbe8-4adb-ade0-54b2896b5260.jpg"
                },
                new Product()
                {
                Name = "KRUX Naos",
                Price = 299,
                Description = "obudowa komputerowa",
                Quantity = 10,
                SubCategoryId = 6,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\df5054d0-03db-48f6-9b6a-17e3c1c09b7d.jpg"
                },
                new Product()
                {
                Name = "SilentiumPC Fera 5",
                Price = 125,
                Description = "chłodzenie",
                Quantity = 10,
                SubCategoryId = 1,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\aa2992df-203c-48c2-addb-a01612124cfe.jpg"
                },
                new Product()
                {
                Name = "GOODRAM 8GB",
                Price = 139,
                Description = "Pamięć RAM",
                Quantity = 10,
                SubCategoryId = 5,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\8f16cb05-0759-4e3b-a68e-280b4f4b8749.jpg"
                },
                new Product()
                {
                Name = "MSI B550",
                Price = 599,
                Description = "płyta główna",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\f91d6d2b-d53a-43f5-ba6b-22d986b07e94.jpg"
                },
                new Product()
                {
                Name = "MSI B550M",
                Price = 399,
                Description = "płyta główna",
                Quantity = 10,
                SubCategoryId = 4,
                File = @"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\de1e4c70-64a2-47e2-ae4a-c55e5339482e.jpg"
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