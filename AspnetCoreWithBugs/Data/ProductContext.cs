using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreWithBugs.Models;

namespace AspnetCoreWithBugs.Data
{
    public class ProductContext : DbContext
    {
        // constructor
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            // default this is empty
        }
        public DbSet<Product> Products { get; set; }
    }
}
