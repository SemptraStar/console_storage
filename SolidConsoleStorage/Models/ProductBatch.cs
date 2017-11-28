

namespace SolidConsoleStorage.Models
{
    class ProductBatch
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        public double Quantity { get; set; }
    }
}
