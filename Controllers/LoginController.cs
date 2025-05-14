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
        
        return RedirectToAction("Index");
    }
    return View(model);
    }

    
   [HttpPost]
public IActionResult Login(LoginModel model)
{
    if (ModelState.IsValid)
    {
        var Users = _context.Users?.FirstOrDefault(u => u.Username == model.Username);
        
        if (Users != null && Users.Password == model.Password)
        {
            
            UserRole role = UserRole.Farmer; 
            
            
            if (Users.Username == "admin" && Users.Password == "adminpass")
            {
                role = UserRole.Employee;
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Users.Username),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, Users.Id.ToString()) 
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true 
            };
            
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
            
           
            if (role == UserRole.Employee)
            {
                return RedirectToAction("Index", "Home"); 
            }
            
            Console.WriteLine("Unknown role, redirecting to login");
            return RedirectToAction("Index", "Login");
            }
        }
        ModelState.AddModelError("", "Invalid username or password");
    }
    
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
       