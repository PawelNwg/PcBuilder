using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Interfaces
{
    internal interface IRepositoryWrapper
    {
        Task Save();

        Task<IDbContextTransaction> BeginTransaction();

        IRepositoryCategory RepositoryCategory { get; }
    }
}