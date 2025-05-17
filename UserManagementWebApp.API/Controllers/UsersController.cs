using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementWebApp.API.Data;
using UserManagementWebApp.API.Models.Domain;
using UserManagementWebApp.API.Models.DTO;
using UserManagementWebApp.API.Repositories.Interface;

namespace UserManagementWebApp.API.Controllers
{
    // https://localhost:xxxx/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            // Handle Role
            Role role;
            
            if (request.Role?.RoleId == null || !await userRepository.AnyRoleAsync(request)) 
            {
                // Create a new role
                role = new Role
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = request.Role.RoleName
                };
                await userRepository.AddRole(role);
            }
            else
            {
                // Use existing role
                role = await userRepository.FindRoleByAsync(request); 
            }

            // Handle Permissions
            var userPermissions = new List<Permission>();

            foreach (var p in request.Permissions)
            {
                Permission permission;

                if (p.PermissionId == Guid.Empty || !await userRepository.AnyPermissionAsync(p))
                {
                    // Create new permission
                    permission = new Permission
                    {
                        PermissionId = Guid.NewGuid(),
                        PermissionName = p.PermissionName,
                        IsReadable = p.IsReadable,
                        IsWritable = p.IsWritable,
                        IsDeletable = p.IsDeletable
                    };
                    await userRepository.AddPermission(permission);
                }
                else
                {
                    // Use existing permission
                    permission = await userRepository.FindPermissionByAsync(p);
                }

                userPermissions.Add(permission);
            }

            // Create the new user
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                UserName = request.UserName,
                Password = request.Password,
                Role = role,
                Permissions = userPermissions
            };

            await userRepository.CreateAsync(user);

            // Response DTO
            var response = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password,
                Role = new RoleDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                },
                Permissions = userPermissions.Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetAllAsync();

            var usersDto = users.Select(user => new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password,
                Role = new RoleDto
                {
                    RoleId = user.Role.RoleId,
                    RoleName = user.Role.RoleName
                },
                Permissions = user.Permissions.Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList()
            });

            return Ok(usersDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password,
                Role = new RoleDto
                {
                    RoleId = user.Role.RoleId,
                    RoleName = user.Role.RoleName
                },
                Permissions = user.Permissions.Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditUser([FromRoute] Guid id, UpdateUserRequestDto request)
        {
            // Handle Role
            Role role;
            if (request.Role?.RoleId == null || !await userRepository.AnyRoleAsync(new CreateUserRequestDto { Role = request.Role }))
            {
                role = new Role
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = request.Role.RoleName
                };
                await userRepository.AddRole(role);
            }
            else
            {
                role = await userRepository.FindRoleByAsync(new CreateUserRequestDto { Role = request.Role });
            }

            // Handle Permissions
            var userPermissions = new List<Permission>();
            foreach (var p in request.Permissions)
            {
                Permission permission;
                if (p.PermissionId == Guid.Empty || !await userRepository.AnyPermissionAsync(p))
                {
                    permission = new Permission
                    {
                        PermissionId = Guid.NewGuid(),
                        PermissionName = p.PermissionName,
                        IsReadable = p.IsReadable,
                        IsWritable = p.IsWritable,
                        IsDeletable = p.IsDeletable
                    };
                    await userRepository.AddPermission(permission);
                }
                else
                {
                    permission = await userRepository.FindPermissionByAsync(p);

                    permission.PermissionName = p.PermissionName;
                    permission.IsReadable = p.IsReadable;
                    permission.IsWritable = p.IsWritable;
                    permission.IsDeletable = p.IsDeletable;

                }

                userPermissions.Add(permission);
            }

            // Create updated user object
            var userToUpdate = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                UserName = request.UserName,
                Password = request.Password,
                Role = role,
                Permissions = userPermissions
            };

            var updatedUser = await userRepository.UpdateAsync(id, userToUpdate);
            if (updatedUser == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserId = updatedUser.UserId,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Phone = updatedUser.Phone,
                UserName = updatedUser.UserName,
                Password = updatedUser.Password,
                Role = new RoleDto
                {
                    RoleId = updatedUser.Role.RoleId,
                    RoleName = updatedUser.Role.RoleName
                },
                Permissions = updatedUser.Permissions.Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName,
                    IsReadable = p.IsReadable,
                    IsWritable = p.IsWritable,
                    IsDeletable = p.IsDeletable
                }).ToList()
            };

            return Ok(userDto);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deletedUser = await userRepository.DeleteAsync(id);

            if (deletedUser == null)
            {
                return NotFound(new DeleteUserResponseDto
                {
                    Status = new StatusDto
                    {
                        Code = "404",
                        Description = "User not found"
                    },
                    Data = new DeleteUserDataDto
                    {
                        Result = false,
                        Message = $"User with id {id} was not found."
                    }
                });
            }

            return Ok(new DeleteUserResponseDto
            {
                Status = new StatusDto
                {
                    Code = "200",
                    Description = "User deleted successfully"
                },
                Data = new DeleteUserDataDto
                {
                    Result = true,
                    Message = $"User with id {id} was successfully deleted."
                }
            });
        }


    }
}
