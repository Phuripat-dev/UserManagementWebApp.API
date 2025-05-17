using Microsoft.EntityFrameworkCore;
using UserManagementWebApp.API.Data;
using UserManagementWebApp.API.Models.Domain;
using UserManagementWebApp.API.Models.DTO;
using UserManagementWebApp.API.Repositories.Interface;

namespace UserManagementWebApp.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> AnyRoleAsync(CreateUserRequestDto request)
        {
            return await dbContext.Roles.AnyAsync(roleId => roleId.RoleId == request.Role.RoleId);
        }

        public async Task<Role> AddRole(Role role)
        {
            dbContext.Roles.Add(role);
            return role;
        }

        public async Task<Role> FindRoleByAsync(CreateUserRequestDto request)
        {
            return await dbContext.Roles.FindAsync(request.Role.RoleId);
        }

        public async Task<bool> AnyPermissionAsync(PermissionDto p)
        {
            return await dbContext.Permissions.AnyAsync(x => x.PermissionId == p.PermissionId);
        }

        public async Task<Permission> AddPermission(Permission permission)
        {
            dbContext.Permissions.Add(permission);
            return permission;
        }

        public async Task<Permission> FindPermissionByAsync(PermissionDto p)
        {
            return await dbContext.Permissions.FindAsync(p.PermissionId);

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Permissions)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> UpdateAsync(Guid id, User user)
        {
            var existingUser = await dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
                return null;

            // Basic fields
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;

            // Update Role (reference only, not duplicating)
            existingUser.Role = user.Role;

            // Clear and reassign Permissions
            existingUser.Permissions.Clear();
            foreach (var p in user.Permissions)
            {
                existingUser.Permissions.Add(p);
            }

            await dbContext.SaveChangesAsync();
            return existingUser;
        }


        //public async Task<User?> DeleteAsync(Guid id)
        //{
        //    var existingUser = await dbContext.Users.FindAsync(id);
        //    if (existingUser == null)
        //    {
        //        return null;
        //    }

        //    dbContext.Users.Remove(existingUser);
        //    await dbContext.SaveChangesAsync();
        //    return existingUser;
        //}

        public async Task<User?> DeleteAsync(Guid id)
        {
            var existingUser = await dbContext.Users
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
            {
                return null;
            }

            dbContext.Permissions.RemoveRange(existingUser.Permissions); 
            dbContext.Users.Remove(existingUser); 

            await dbContext.SaveChangesAsync();
            return existingUser;
        }


        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await dbContext.Permissions.ToListAsync();
        }


    }
}
