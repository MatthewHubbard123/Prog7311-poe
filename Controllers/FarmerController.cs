using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Prog7311.Poe.Models;
using Prog7311_POE.Data;

namespace Prog7311_POE.Controllers
{
    public class FarmerController : Controller
    {
        public readonly ApplicationDbContext _context;
        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Farmer()
        {
            return View();
        }

        [HttpPost]
        [Route("AddFarmer")] 
        public IActionResult AddFarmer(UserModel model)
        {
            if (ModelState.IsValid)
            {
            var Users = new UserModel
            {
                FarmerName = model.FarmerName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email.ToString(),
                Username = model.Username,
                Password = model.Password,
                FarmName = model.FarmName,
                FarmAddress = model.FarmAddress,
                FarmLocation = model.FarmLocation,
                FarmSize = model.FarmSize,
                Role = UserRole.Farmer
            };
            

            _context.Users.Add(Users);
            _context.SaveChanges();
        
            TempData["SuccessMessage"] = "Farmer added successfully!";
            return RedirectToAction("Index", "Farmer"); 
        }
        return View(model);
        }
    }
}
