using Microsoft.EntityFrameworkCore.Storage;
using PcBuilder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    public interface IRepositoryWrapper
    {
        Task Save();

        Task<IDbContextTransaction> BeginTransaction();

        IRepositoryCategory RepositoryCategory { get; }
        IRepositoryProduct RepositoryProduct { get; }
        IRepositorySubCategory RepositorySubcategory { get; }
    }
}