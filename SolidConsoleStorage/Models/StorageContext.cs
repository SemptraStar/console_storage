using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SolidConsoleStorage.Models
{
    class StorageContext : DbContext, IStorageContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().HasIndex(u => u.UnitName).IsUnique(true);
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<ProductBatch>().HasKey(pb => new { pb.ProductId, pb.BatchId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
