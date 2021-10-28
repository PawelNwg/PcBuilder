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
    public class RepositoryDetailedDataProduct : RepositoryDatabase<DetailedDataProduct>, IRepositoryDetailedDataProduct
    {
        public RepositoryDetailedDataProduct(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<DetailedDataProduct>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<List<DetailedDataProduct>> GetByCondition(Expression<Func<DetailedDataProduct, bool>> expression)
        {
            return await FindByCondition(expression).ToListAsync();
        }

        public async Task<DetailedDataProduct> GetById(int id)
        {
            return await FindByCondition(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DetailedDataProduct> GetOneByCondition(Expression<Func<DetailedDataProduct, bool>> expression)
        {
            return await FindOneByCondition(expression);
        }
    }
}