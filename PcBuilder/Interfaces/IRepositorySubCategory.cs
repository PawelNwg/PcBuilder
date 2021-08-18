using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositorySubCategory : IRepositoryDatabase<Subcategory>
    {
        Task<List<Subcategory>> GetAll();

        Task<Subcategory> GetById(int id);

        Task<List<Subcategory>> GetByCondition(System.Linq.Expressions.Expression<Func<Subcategory, bool>> expression);
    }
}