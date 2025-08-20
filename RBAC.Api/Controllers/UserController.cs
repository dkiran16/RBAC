using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBAC.Api.Models.Dto;
using RBAC.Api.Services;
using System.Security.Cryptography;
using System.Text;

namespace RBAC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var data = userService.GetUsers();
            return Ok(data);
        }

        [HttpPost("AddUser")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult AddUser([FromBody] AddUserDto dto)
        {
            var hashPassword = HashPassword(dto.Password);
            dto.Password = hashPassword;
            var user = userService.AddUser(dto);
            return Ok(user);

        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpPut(nameof(UpdateUser))]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult UpdateUser([FromBody] GetUserDto user)
        {
            var result = userService.UpdateUser(user); 
            return Ok("User updated successfully.");
        }

        [HttpDelete( $"{nameof(DeleteUser)}/" + "{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = userService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }


    }
}
