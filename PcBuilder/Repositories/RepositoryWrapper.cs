using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using PcBuilder.Repositories.Interfaces;
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

        private IRepositorySubCategory repositorySubCategory;

        public IRepositorySubCategory RepositorySubcategory
        {
            get
            {
                if (repositorySubCategory == null)
                    repositorySubCategory = new RepositorySubcategory(_context);
                return repositorySubCategory;
            }
        }

        private IRepositoryDetailedDataProduct repositoryDetailedDataProduct;

        public IRepositoryDetailedDataProduct RepositoryDetailedDataProduct
        {
            get
            {
                if (repositoryDetailedDataProduct == null)
                    repositoryDetailedDataProduct = new RepositoryDetailedDataProduct(_context);
                return repositoryDetailedDataProduct;
            }
        }

        private IRepositoryOffer repositoryOffer;

        public IRepositoryOffer RepositoryOffer
        {
            get
            {
                if (repositoryOffer == null)
                    repositoryOffer = new RepositoryOffer(_context);
                return repositoryOffer;
            }
        }

        private IRepositoryOrder repositoryOrder;

        public IRepositoryOrder RepositoryOrder
        {
            get
            {
                if (repositoryOrder == null)
                    repositoryOrder = new RepositoryOrder(_context);
                return repositoryOrder;
            }
        }

        private IRepositoryUser repositoryUser;

        public IRepositoryUser RepositoryUser
        {
            get
            {
                if (repositoryUser == null)
                    repositoryUser = new RepositoryUser(_context);
                return repositoryUser;
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