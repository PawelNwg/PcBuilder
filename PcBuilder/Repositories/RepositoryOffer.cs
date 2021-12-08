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
    public class RepositoryOffer : RepositoryDatabase<Offer>, IRepositoryOffer
    {
        public RepositoryOffer(ApplicationDbContext context) : base(context)
        {
        }

        public void Add(Offer offer)
        {
            Create(offer);
        }

        public void DeleteOffer(Offer offer)
        {
            Delete(offer);
        }

        public async Task<List<Offer>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<List<Offer>> GetByCondition(Expression<Func<Offer, bool>> expression)
        {
            return await FindByCondition(expression).ToListAsync();
        }

        public async Task<Offer> GetById(int id)
        {
            return await FindByCondition(w => w.OfferId == id).FirstOrDefaultAsync();
        }

        public async Task SaveOffer()
        {
            await Save();
        }

        public void UpdateOffer(Offer offer)
        {
            Delete(offer);
        }
    }
}
