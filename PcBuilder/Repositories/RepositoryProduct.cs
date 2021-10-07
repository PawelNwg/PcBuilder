using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Services.ImageToBlobStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PcBuilder.Repositories
{
    public class RepositoryProduct : RepositoryDatabase<Product>, IRepositoryProduct
    {
        private readonly IImageService imageService;

        public RepositoryProduct(ApplicationDbContext context, IImageService imageService) : base(context)
        {
            this.imageService = imageService;
        }

        public async Task<List<Product>> GetAll()
        {
            var list = await FindAll().ToListAsync();
            list.ForEach(x => x.File = imageService.GetImage(x.File));
            return list;
        }

        public async Task<Product> GetById(int id)
        {
            var list = await FindByCondition(w => w.ProductId == id).FirstOrDefaultAsync();
            list.File = imageService.GetImage(list.File);
            return list;
        }

        public async Task<List<Product>> GetByCategory(int subcategory)
        {
            return await FindByCondition(c => c.SubCategoryId == subcategory).ToListAsync();
        }

        public async Task<List<Product>> GetByCondition(Expression<Func<Product, bool>> expression)
        {
            var list = await FindByCondition(expression).ToListAsync();
            foreach (var item in list)
            {
                if (item.File != null || item.File != "")
                    item.File = imageService.GetImage(item.File);
            }
            return list;
        }
    }
}