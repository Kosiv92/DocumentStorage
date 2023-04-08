using DocumentStorageMVC.Core;
using DocumentStorageMVC.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentStorageMVC.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Document> Documents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DocumentConfiguration());
            builder.Entity<Document>().ToTable("Documents");
            base.OnModelCreating(builder);
        }
    }
}
