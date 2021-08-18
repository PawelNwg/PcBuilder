using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PcBuilder.Repositories
{
    public class RepositorySubcategory : RepositoryDatabase<Subcategory>, IRepositorySubCategory
    {
        public RepositorySubcategory(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Subcategory> GetById(int id)
        {
            return await FindByCondition(w => w.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Subcategory>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<List<Subcategory>> GetByCondition(Expression<Func<Subcategory, bool>> expression)
        {
            return await FindByCondition(expression).ToListAsync();
        }
    }
}