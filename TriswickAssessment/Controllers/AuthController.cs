using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Data;
using TriswickAssessment.Models;
using System.Security.Claims;

namespace TriswickAssessment.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly DataContext _context;

        public AuthController( DataContext context)
        {
            _context = context;
        }

        //register new user
        //[HttpPost("register/{username}/{password}")]
        //public async Task<IActionResult> Register(string username, string password)
        //{
        //    var user = new UserModel
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Username = username,
        //        Password = password,
        //        UserRole = "Regular",
        //    };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            // Check if username already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.UserName);
            if (existingUser != null)
            {
                return Conflict(new { message = "Username already taken." });
            }

            // Create new user
            var user = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.UserName,
                Password = model.Password, // Note: In a real application, make sure to hash the password!
                UserRole = "Regular"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully",
                user.Id,
                user.Username,
                user.UserRole
            });
        }


        //TODO: on login check if user is Mod?
        //Login user
        //[HttpPost("login/{username}/{password}")]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    try
        //    {
        //        if (username == "moderator" && password == "modpassword")
        //        {
        //            return Ok(new
        //            {
        //                message = "Logged in as Moderator",
        //                Username = username,
        //                Role = "Moderator"
        //            });
        //        }

        //        var user = await _context.Users
        //            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        //        if (user == null)
        //        {
        //            return Unauthorized("Invalid username or password.");
        //        }

        //        return Ok(new
        //        {
        //            message = "Login successful",
        //            Username = user.Username,
        //            Role = user.UserRole
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Catch login Error:", ex.Message);
        //        return StatusCode(500, "Internal server error. Please try again later.");
        //    }
        //}

        [HttpPost("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            try
            {
                // Mod User
                if (username == "moderator" && password == "modpassword")
                {
                    var claims = new[]
                    {
                         new Claim(ClaimTypes.Name, username),
                         new Claim(ClaimTypes.Role, "Moderator")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

                    return Ok(new
                    {
                        message = "Logged in as Moderator",
                        UserName = username,
                        Role = "Moderator"
                    });
                }

                // User
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                var userClaims = new[]
                {
                     new Claim(ClaimTypes.Name, user.Username),
                     new Claim(ClaimTypes.Role, user.UserRole)
                 };

                var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var userAuthProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), userAuthProperties);

                return Ok(new
                {
                    message = "Login successful",
                    UserName = user.Username,
                    Role = user.UserRole
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catch login Error:", ex.Message);
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }




        //Logout user
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }


    }
}
