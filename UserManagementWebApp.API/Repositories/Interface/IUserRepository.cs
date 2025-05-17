using UserManagementWebApp.API.Models.Domain;
using UserManagementWebApp.API.Models.DTO;

namespace UserManagementWebApp.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<bool> AnyRoleAsync(CreateUserRequestDto request);
        Task<Role> AddRole(Role role);
        Task<Role> FindRoleByAsync(CreateUserRequestDto request);
        Task<bool> AnyPermissionAsync(PermissionDto p);
        Task<Permission> AddPermission(Permission permission);
        Task<Permission> FindPermissionByAsync(PermissionDto p);

        //The rest of CRUD
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> UpdateAsync(Guid id, User user);
        Task<User?> DeleteAsync(Guid id);

        Task<List<Role>> GetAllRolesAsync();
        Task<List<Permission>> GetAllPermissionsAsync();

    }
}
