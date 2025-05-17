using Microsoft.AspNetCore.Mvc;
using UserManagementWebApp.API.Models.DTO;
using UserManagementWebApp.API.Repositories.Interface;

namespace UserManagementWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public PermissionsController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await userRepository.GetAllPermissionsAsync();

            var response = permissions.Select(p => new PermissionDto
            {
                PermissionId = p.PermissionId,
                PermissionName = p.PermissionName,
                IsReadable = p.IsReadable,
                IsWritable = p.IsWritable,
                IsDeletable = p.IsDeletable
            }).ToList();

            return Ok(response);
        }
    }
}
