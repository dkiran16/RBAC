using Microsoft.EntityFrameworkCore;
using RBAC.Api.Data;
using RBAC.Api.Models;
using RBAC.Api.Models.Dto;

namespace RBAC.Api.Services
{
    public interface IRoleService
    {
        List<RoleDto> GetRoles();
        int AddRole(RoleDto role);
        int UpdateRole(RoleDto role);
        int DeleteRole(int Id);


    }
    public class RoleService(AppDbContext db) : IRoleService
    {
        public List<RoleDto> GetRoles() 
        {
            var roles = db.Roles.AsNoTracking()
                .Select(p=> new RoleDto { 
                    Id = p.Id, 
                    Name = p.Name, 
                    Description = $"desc - {p.Name}" 
                }).ToList();
            return roles;
        }

        public int AddRole(RoleDto role)
        {
            var newRole = new Role { Name = role.Name };
            var roles = db.Roles.Add(newRole);
            var dbStatus = db.SaveChanges();
            return dbStatus;
        }

        public int UpdateRole(RoleDto role)
        {
            var existingRole = db.Roles.FirstOrDefault(r => r.Id == role.Id);
            existingRole.Name = role.Name;
            existingRole.Id = role.Id;
            db.Roles.Update(existingRole);
            return db.SaveChanges();
        }

        public int DeleteRole(int Id)
        {
            var role = db.Roles.FirstOrDefault(r => r.Id == Id);
            db.Roles.Remove(role);
            return db.SaveChanges();
        }

    }
}
