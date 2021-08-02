using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        private IRepositoryCategory repositoryDatabase;

        public IRepositoryCategory RepositoryCategory
        {
            get
            {
                if (repositoryDatabase == null)
                    repositoryDatabase = new RepositoryCategory(_context);
                return repositoryDatabase;
            }
        }

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}