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
    public class RepositoryCategory : RepositoryDatabase<Category>, IRepositoryCategory
    {
        public RepositoryCategory(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int id)
        {
            return await FindByCondition(w => w.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public Task<List<Category>> GetByCondition(Expression<Func<Category, bool>> expression)
        {
            return FindByCondition(expression).ToListAsync();
        }

        public async Task<Category> GetOneByCodition(Expression<Func<Category, bool>> expression)
        {
            return await FindOneByCondition(expression);
        }
    }
}