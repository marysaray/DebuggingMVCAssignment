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
            return View(); // create new view page for adding product
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid) // checks to see if it is a valid product
            {
                await _context.AddAsync(product); // create new product

                await _context.SaveChangesAsync(); // add to database

                TempData["Message"] = $"{product.Name} was added successfully!";

                return RedirectToAction(nameof(Index)); // goes back to catalog page
            }
            return View(product); // stays on the same page
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) // unique identifier to edit 
        {
            var product = await _context.Products.FindAsync(id); // check database by id
            if (product == null) // not available
            {
                return NotFound(); // return message
            }
            return View(product); // model passed in: dispay current product
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product) // for all properties except id
        {
            if (ModelState.IsValid) // ensure validation product exists in the database
            {
                _context.Update(product); // update product in database
                await _context.SaveChangesAsync(); // save the changes 

                TempData["Message"] = $"{product.Name} was edited successfully!";

                return RedirectToAction(nameof(Index)); // return to the catalog page
            }
            return View(product); // model passed in: display current product
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) // unique identifier to edit 
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id); // check database by id

            if (product == null) // not available
            {
                return NotFound(); // return message
            }

            return View(product); // model passed in: dispay current product
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) // unique identifier
        {
            var product = await _context.Products.FindAsync(id); // get product from database
            _context.Products.Remove(product); // delete product
           await _context.SaveChangesAsync(); // save changes: query to database

            TempData["Message"] = $"{product.Name} has been deleted.";
            return RedirectToAction(nameof(Index)); // return back to view
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}