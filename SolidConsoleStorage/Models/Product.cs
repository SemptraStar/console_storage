using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolidConsoleStorage.Models
{
    class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        protected ICollection<ProductBatch> ProductBatches { get; set; }
    }
}
