using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreWithBugs.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(35)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
