using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositoryCategory : IRepositoryDatabase<Category>
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(int id);
    }
}