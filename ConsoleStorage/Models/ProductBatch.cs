using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleStorage.Models
{
    public class ProductBatch
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        public double Quantity { get; set; }
    }
}
