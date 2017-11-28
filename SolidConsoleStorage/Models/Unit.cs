using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolidConsoleStorage.Models
{
    class Unit
    {
        [Key]
        public int Id { get; set; }

        public string UnitName { get; set; }

        protected ICollection<Product> Products { get; set; }
    }
}
