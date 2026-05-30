using Job_Application_Tracker.Data;
using Job_Application_Tracker.DTOs;
using Job_Application_Tracker.Models;
using Job_Application_Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Job_Application_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        public AuthController(AppDbContext context, TokenService service)
        {
            _context = context;
            _tokenService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync( x => x.Email == register.Email);
            if(existingUser != null)
            {
                return BadRequest("User already registered!");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var user = new User()
            {
                Email = register.Email,
                PasswordHash = passwordHash,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);
            return Ok(new {token});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var userexists = await _context.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
            if(userexists == null)
            {
                return Unauthorized("Invalid Username or password");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, userexists.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid Username or password");
            var token = _tokenService.GenerateToken(userexists);
            return Ok(new {token});
                 
           // BCrypt.Net.BCrypt.EnhancedVerify(lo)
        }
    }
}
