using UserManagementWebApp.API.Models.Domain;

namespace UserManagementWebApp.API.Models.DTO
{
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public RoleDto Role { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}
