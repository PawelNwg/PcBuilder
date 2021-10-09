using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositoryProduct : IRepositoryDatabase<Product>
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(int id);

        Task<List<Product>> GetByCondition(System.Linq.Expressions.Expression<Func<Product, bool>> expression);

        void Add(Product product);

        void UpdateProduct(Product product);

        void SaveProduct();
    }
}