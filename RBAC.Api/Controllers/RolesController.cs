using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBAC.Api.Models.Dto;
using RBAC.Api.Services;

namespace RBAC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var roles = roleService.GetRoles();
            return Ok(roles);
        }

        [HttpPost("PostRoles")]

        public IActionResult AddRole([FromBody] RoleDto dto)
        {
            var roles = roleService.AddRole(dto);
            return Ok(roles);
        }

        [HttpPut]
        public IActionResult UpdateRole([FromBody] RoleDto role)
        {
            var result = roleService.UpdateRole(role);
            return Ok("Role updated successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRole(int id)
        {
            var result = roleService.DeleteRole(id);
            return Ok("Role Deleted SuccessFully.");
        }
    }
}
