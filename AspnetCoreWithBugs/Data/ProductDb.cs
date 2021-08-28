using AspnetCoreWithBugs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreWithBugs.Data
{
    public static class ProductDb
    {
        /// <summary>
        /// Returns the total count of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        public static async Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await(from p in _context.Products // get the amount of products
                         select p).CountAsync();
        }

        /// <summary>
        /// Get specified amount of products in a page
        /// </summary>
        /// <param name="_context">Database context to use</param>
        /// <param name="pageSize">The number of products per page</param>
        /// <param name="pageNum">Page of products to return</param>
        /// <returns></returns>
        public static async Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        { 
            return await (_context.Products.Skip(pageSize * (pageNum - 1))
                        .Take(pageSize).ToListAsync());
        }

        /// <summary>
        /// Create new product to database
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<Product> CreateProductAsync(ProductContext _context, Product product)
        {
            await _context.AddAsync(product); // create new product
            await _context.SaveChangesAsync(); // add to database

            return product;
        }

    }
}