namespace UserManagementWebApp.API.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public RoleDto Role { get; set; } = new RoleDto();
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
