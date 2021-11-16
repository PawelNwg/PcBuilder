using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PcBuilder.Repositories
{
    public class RepositoryUser : RepositoryDatabase<AppUser>, IRepositoryUser
    {
        public RepositoryUser(ApplicationDbContext context) : base(context)
        {
        }

        public void Add(AppUser user)
        {
            Create(user);
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public Task<List<AppUser>> GetByCondition(Expression<Func<AppUser, bool>> expression)
        {
            return FindByCondition(expression).ToListAsync();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await FindByCondition(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetOneByCodition(Expression<Func<AppUser, bool>> expression)
        {
            return await FindOneByCondition(expression);
        }

        public async Task SaveUser()
        {
            await Save();
        }

        public void UpdateUser(AppUser user)
        {
            Update(user);
        }
    }
}
