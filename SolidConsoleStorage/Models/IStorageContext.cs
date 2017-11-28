using Microsoft.EntityFrameworkCore;

namespace SolidConsoleStorage.Models
{
    interface IStorageContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<ProductBatch> ProductBatches { get; set; }

        int SaveChanges();
    }
}
