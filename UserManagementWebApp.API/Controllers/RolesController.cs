using Microsoft.AspNetCore.Mvc;
using UserManagementWebApp.API.Models.DTO;
using UserManagementWebApp.API.Repositories.Interface;

namespace UserManagementWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public RolesController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await userRepository.GetAllRolesAsync();

            var response = roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();

            return Ok(response);
        }
    }
}
