using Microsoft.EntityFrameworkCore;
using RBAC.Api.Controllers;
using RBAC.Api.Data;
using RBAC.Api.Models;
using RBAC.Api.Models.Dto;
using System.Security.Cryptography;
using System.Text;

namespace RBAC.Api.Services
{
    public interface IUserService
    {
        List<GetUserDto> GetUsers();
        int AddUser(AddUserDto user);
        int UpdateUser(GetUserDto user);
        int DeleteUser(int Id);
    }
    public class UserService(AppDbContext db) : IUserService
    {
        public List<GetUserDto> GetUsers()
        {
            var users = db.Users.Include(p => p.Role).AsNoTracking().ToList();

            var u1 = users.Select(p => new GetUserDto
            {
                    Id = p.Id,
                    Username = p.Username,
                    RoleId = p.RoleId,
                    RoleName = p.Role.Name
                }).ToList();
            return u1;
        }

        public int AddUser(AddUserDto user)
        {
            var newUser = new User { 
                Username = user.Username , 
                PasswordHash = user.Password,
                RoleId = user.RoleId
            };
            var users = db.Users.Add(newUser);
            var dbStatus = db.SaveChanges();
            return dbStatus;
        }

        public int UpdateUser(GetUserDto user)
        {
            var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            existingUser.Username = user.Username;
            existingUser.RoleId = user.RoleId;

            db.Users.Update(existingUser);
            var dbStatus = db.SaveChanges();

            return dbStatus;
        }

        public int DeleteUser(int Id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == Id);
            db.Users.Remove(user);
            return db.SaveChanges();
        }



    }
}
