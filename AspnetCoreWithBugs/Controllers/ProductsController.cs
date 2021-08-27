using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetCoreWithBugs.Models;
using AspnetCoreWithBugs.Data;

namespace AspnetCoreWithBugs.Controllers
{
    public class ProductsController : Controller
    {
        // field for accessibility: class scope for multiple methods
        private readonly ProductContext _context; // private not available to another class

   
        /// <summary>
        /// constructor injection: inject services DbContext (dependency injection)
        /// easier maintainability for applications
        /// </summary>
        /// <param name="context"></param>
        public ProductsController(ProductContext context) // framework calls the constructor: startup.cs file
        {
            _context = context; // associated with startup.cs file: ConfigureServices
        }

        /// <summary>
        /// Displays list of all products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() // right click to go to view
        {
            // gets all product from database and sends to the view page
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid) // checks to see if it is a valid product
            {
                await _context.AddAsync(product);   // adding new product
                return RedirectToAction(nameof(Index)); // stays on the same page?
            }
            return View(product); // display new product
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
 
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
