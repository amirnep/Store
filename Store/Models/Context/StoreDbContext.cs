using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Models.Entities;
using Store.Models.Entities.Contact_Us;
using Store.Models.Entities.Invoice;
using Store.Models.Entities.Invoices;
using Store.Models.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductsGallery> ProductsGalleries { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<OtherSizes> OtherSizes { get; set; }
        public DbSet<Sizes> Sizes { get; set; }

        public DbSet<OtherColors> OtherColors { get; set; }
        public DbSet<Colors> Colors { get; set; }

        public DbSet<OtherCategories> OtherCategories { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceSub> InvoiceSubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
