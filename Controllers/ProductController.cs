using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prog7311_POE.Models;
using Prog7311_POE.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Prog7311_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var product = new ProductModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Category = model.Category,
                    ProductionDate = model.ProductionDate,
                    Stock = model.Stock,
                    Description = model.Description,
                    UserId = userId
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction("Index", "Product");
            }
            return View(model);
        }

        
        public IActionResult Scope()
        {
            
            if (User.IsInRole("Employee"))
            {
                
                var allProducts = _context.Products.Include(p => p.FarmerName).ToList();
                
                if (allProducts.Count == 0)
                {
                    ViewBag.Message = "No products found in the system.";
                }
                
                return View("EmployeeOverview", allProducts);
            }
            else 
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
                
                var products = _context.Products.Include(p => p.FarmerName).Where(p => p.UserId == userId).ToList();
        
                if (products.Count == 0)
                {
                    ViewBag.Message = "No products found for this user.";
                }
        
                return View("FarmerOverview", products);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Scope");
            }
    
            if (User.IsInRole("Employee"))
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            else
            {
            TempData["ErrorMessage"] = "You do not have permission to delete this product.";
            }
    
            return RedirectToAction("Scope");
        }
    }
}