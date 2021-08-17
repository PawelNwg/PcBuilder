using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using PcBuilder.Services.ImageToBlobStorage;
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
        private readonly IImageService _imageService;

        public IRepositoryCategory RepositoryCategory
        {
            get
            {
                if (repositoryDatabase == null)
                    repositoryDatabase = new RepositoryCategory(_context);
                return repositoryDatabase;
            }
        }

        private IRepositoryProduct repositoryProductDatabase;

        public IRepositoryProduct RepositoryProduct
        {
            get
            {
                if (repositoryProductDatabase == null)
                    repositoryProductDatabase = new RepositoryProduct(_context, _imageService);
                return repositoryProductDatabase;
            }
        }

        public RepositoryWrapper(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
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