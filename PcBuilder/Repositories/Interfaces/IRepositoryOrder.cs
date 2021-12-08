using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Repositories.Interfaces
{
    public interface IRepositoryOrder: IRepositoryDatabase<Order>
    {
        Task<List<Order>> GetAll();

        Task<Order> GetById(int id);

        Task<List<Order>> GetByCondition(System.Linq.Expressions.Expression<Func<Order, bool>> expression);

        void Add(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);

        Task SaveOrder();
    }
}
