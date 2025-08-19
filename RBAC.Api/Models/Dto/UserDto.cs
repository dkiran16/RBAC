namespace RBAC.Api.Models.Dto;

public class UserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class AddUserDto : UserDto
{
    public int RoleId { get; set; }
}

public class GetUserDto : UserDto
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}

public class RegisterUserDto : UserDto
{
}