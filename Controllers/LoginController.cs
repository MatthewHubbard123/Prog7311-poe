using Microsoft.AspNetCore.Mvc;
using Prog7311_POE.Models;
using Prog7311_POE.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Prog7311.Poe.Models;
using System.Linq;


namespace Prog7311_POE.Controllers
{
    public class LoginController : Controller
    {
        public readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

    [HttpPost]
    public IActionResult Register(UserModel model)
    {
    // Validate the model
    if (ModelState.IsValid)
    {
        // create a new user
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
        //add the user to the database
        _context.Users.Add(Users);
        //save the changes
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }
    return View(model);
    }

    
   [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
        // Check if the user exists in the database
        var Users = _context.Users?.FirstOrDefault(u => u.Username == model.Username);
            
        // Check if the user exists and the password and matches the model password   
        if (Users != null && Users.Password == model.Password)
            {
            //
            UserRole role = UserRole.Farmer; 
            
            // Check if the hardcoded user is an admin
            if (Users.Username == "admin" && Users.Password == "adminpass")
            {
                role = UserRole.Employee;
            }
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Users.Username),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, Users.Id.ToString()) 
            };
            // Create claims identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true 
            };
            // Sign in the user
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
            
            // Redirect based on role
            if (role == UserRole.Employee)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else if (role == UserRole.Farmer)
            {
                return RedirectToAction("Farm", "Home"); 
            }
            else
            {
                return RedirectToAction("Index", "Login"); 
            }
        }
        }
        ModelState.AddModelError("", "Invalid username or password");
        return View("Index", model);
    }

    public async Task<IActionResult> Logout()
    {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Index", "Login");
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }

    }
}
       