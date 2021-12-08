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
    public class RepositoryOrder : RepositoryDatabase<Order>, IRepositoryOrder
    {
        public RepositoryOrder(ApplicationDbContext context) : base(context)
        {
        }

        public void Add(Order order)
        {
            Create(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<List<Order>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<List<Order>> GetByCondition(Expression<Func<Order, bool>> expression)
        {
            return await FindByCondition(expression).ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await FindByCondition(w => w.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task SaveOrder()
        {
            await Save();
        }

        public void UpdateOrder(Order order)
        {
            Delete(order);
        }
    }
}
