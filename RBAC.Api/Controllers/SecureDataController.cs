using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RBAC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecureDataController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "Confidential Admin data" });
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet("editor-access")]
        public IActionResult EditorAccess()
        {
            return Ok(new { message = "Editor and Admin access data" });
        }

        [Authorize]
        [HttpGet("all-users")]
        public IActionResult AllUsers()
        {
            return Ok(new { message = "Accessible to all logged-in users" });
        }
    }
}
