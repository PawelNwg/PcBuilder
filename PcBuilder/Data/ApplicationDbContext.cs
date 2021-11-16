using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PcBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
            builder.Entity<OrderOffers>(entity =>
             {
                 entity.HasKey(e => new { e.OrderId, e.OfferId });
             });
            builder.Entity<Product>(entity =>
            {
                entity.HasMany(x => x.DetailedDataProducts).WithOne(y => y.Product).OnDelete(DeleteBehavior.Cascade);
            });
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CompanyOffer> CompanyOffers { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderOffers> OrderOffers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<UserOffer> UserOffers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<DetailedDataProduct> DetailedDataProducts { get; set; }
    }
}