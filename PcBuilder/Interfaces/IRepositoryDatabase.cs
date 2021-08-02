using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositoryDatabase<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> FindAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        Task<bool> Exists(int id);
    }
}