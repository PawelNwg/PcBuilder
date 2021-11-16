using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Repositories.Interfaces
{
    public interface IRepositoryUser : IRepositoryDatabase<AppUser>
    {
        Task<List<AppUser>> GetAll();

        Task<AppUser> GetById(string id);

        Task<List<AppUser>> GetByCondition(System.Linq.Expressions.Expression<Func<AppUser, bool>> expression);

        Task<AppUser> GetOneByCodition(System.Linq.Expressions.Expression<Func<AppUser, bool>> expression);

        void Add(AppUser user);

        void UpdateUser(AppUser user);

        Task SaveUser();

    }
}
