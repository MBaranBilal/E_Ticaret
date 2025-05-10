using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Ticaret.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class ProductController : Controller
    {
        private readonly ETicaretDbContext _context;

        public ProductController(ETicaretDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("AdminPage", "Admin");
            }

            // Hataları yazdır
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Console.WriteLine("❌ Validation error: " + error);
            }

            // Geri dön
            ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();

            return RedirectToAction("AdminPage", "Admin");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("AdminPage", "Admin");
        }

       




    }
}
