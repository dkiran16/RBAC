using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBAC.Api.Data;
using RBAC.Api.Models;
using RBAC.Api.Models.Dto;
using RBAC.Api.Services;
using System.Security.Cryptography;
using System.Text;

namespace RBAC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return Conflict(new { message = "Username already taken." });

            var viewerRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Viewer");
            if (viewerRole == null)
                return StatusCode(500, new { message = "Default Viewer role missing." });

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                RoleId = viewerRole.Id
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterUserDto dto)
        {
            var user = await _db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                return Unauthorized(new { message = "Invalid credentials." });

            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
