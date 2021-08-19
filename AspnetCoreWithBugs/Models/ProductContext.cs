using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreWithBugs.Models;

namespace AspnetCoreWithBugs.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<AspnetCoreWithBugs.Models.Product> Product { get; set; }
    }
}
