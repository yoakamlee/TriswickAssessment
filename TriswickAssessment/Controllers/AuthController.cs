using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Data;
using TriswickAssessment.Models;

namespace TriswickAssessment.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        private readonly DataContext _context;

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //TODO: Add user roles Mod users - yls
        //register new user
        [HttpPost("register/{username}/{password}")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = password,
                UserRole = "Regular",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        //TODO: on login check if user is Mod?
        //Login user
        [HttpPost("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //Mod User (For testing atm) - yls
            if (username == "moderator" && password == "modpassword")
            {
                return Ok(new
                {
                    message = "Logged in as Moderator",
                    Username = username,
                    Role = "Moderator"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if(user == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                message = "Login successful",
                Username = user.Username,
                Role = user.UserRole
            });
        }

        
    }
}
