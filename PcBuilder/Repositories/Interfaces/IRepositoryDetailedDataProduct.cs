using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositoryDetailedDataProduct : IRepositoryDatabase<DetailedDataProduct>
    {
        Task<List<DetailedDataProduct>> GetAll();

        Task<DetailedDataProduct> GetById(int id);

        Task<List<DetailedDataProduct>> GetByCondition(System.Linq.Expressions.Expression<Func<DetailedDataProduct, bool>> expression);

        Task<DetailedDataProduct> GetOneByCondition(System.Linq.Expressions.Expression<Func<DetailedDataProduct, bool>> expression);
    }
}