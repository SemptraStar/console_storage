using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolidConsoleStorage.Models
{
    class Batch
    {
        [Key]
        public int Id { get; set; }

        public bool IsDelivery { get; set; }
        public DateTime Date { get; set; }

        protected ICollection<Product> Products { get; set; }
    }
}
