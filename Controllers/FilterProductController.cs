 using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Prog7311_POE.Data;
    using Prog7311_POE.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

namespace Prog7311_POE.Controllers
{
   

    public class FilterProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("FilterProductsByFarmer")]
        public IActionResult FilterProductsByFarmer(FilterViewModel filter)
        {

        bool noFiltersApplied = string.IsNullOrEmpty(filter.FarmerName) &&
        string.IsNullOrEmpty(filter.ProductName) &&
        string.IsNullOrEmpty(filter.Category) &&
        filter.ProductionDate == default;
    
        if (noFiltersApplied)
        {
            ViewData["NoFiltersApplied"] = true;
            return View("Results", new List<FilterViewModel>());
        }

            var products = _context.Products.AsQueryable();
            var farmers = _context.Users.AsQueryable();
        
            if (!string.IsNullOrEmpty(filter.FarmerName))
            {
                farmers = farmers.Where(p => p.FarmerName.Contains(filter.FarmerName));
            }

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(filter.ProductName));
            }

            if (!string.IsNullOrEmpty(filter.Category))
            {
                products = products.Where(p => p.Category.Contains(filter.Category));
            }

            if (filter.ProductionDate != default)
            {
            DateOnly productionDate = DateOnly.FromDateTime(filter.ProductionDate);
            products = products.Where(p => p.ProductionDate == productionDate);
            }

        var filteredProducts = products.ToList();

        var viewModels = filteredProducts.Select(p => new FilterViewModel 
        {
            ProductName = p.ProductName,
            Price = p.Price,
            Category = p.Category,
            ProductionDate = p.ProductionDate.ToDateTime(TimeOnly.MinValue),
            Stock = p.Stock,
            Description = p.Description,
            FarmerName = p.FarmerName?.FarmerName ?? "Unknown"
        }).ToList();

        return View("Results", viewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}