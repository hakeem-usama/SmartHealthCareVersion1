
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCare.DataAccess;
using SmartHealthCare.Models;

namespace SmartHealthCare.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseRepository _databaseRepository;

        public UserController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        // Displays registration form
        public IActionResult Register()
        {
            return View();
        }

        // Registers a new user
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.AddUserAsync(user);
                return RedirectToAction("Login"); // Redirect to Login after successful registration
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Processes the login attempt
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _databaseRepository.GetUserByEmailAsync(email);
            if (user != null && user.PasswordHash == password) // Simplified password check
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                // Set up session or authentication logic here
                // Redirect to the home page after successful login
                return View();
            }
            return RedirectToAction("Index", "Home");


        }
        // Displays user profile
        public async Task<IActionResult> Profile(int userId)
        {
            var user = await _databaseRepository.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return View(user);
        }

        // Updates user profile
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User user)
        {
            if (ModelState.IsValid)
            {
                await _databaseRepository.UpdateUserAsync(user);
                return RedirectToAction("Profile", new { userId = user.UserID });
            }
            return View("Profile", user);
        }
    }
}