using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ConsoleStorage.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public decimal UnitPrice { get; set; }

        protected ICollection<ProductBatch> Batches { get; set; }
    }
}
