using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Repositories.Interfaces
{
    public interface IRepositoryOffer : IRepositoryDatabase<Offer>
    {
        Task<List<Offer>> GetAll();

        Task<Offer> GetById(int id);

        Task<List<Offer>> GetByCondition(System.Linq.Expressions.Expression<Func<Offer, bool>> expression);

        void Add(Offer offer);

        void UpdateOffer(Offer offer);

        void DeleteOffer(Offer offer);

        Task SaveOffer();
    }
}
