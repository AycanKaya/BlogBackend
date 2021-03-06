using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAllUsers")]
         public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserInRole")]
        public async Task<IActionResult> GetUsersInRoles()
        {
            var userWithRole = await _adminService.GetUsersWithRole();
             return Ok(userWithRole);
        }
         
         [HttpPut("AddRoleToUser")]
         public async Task<IActionResult> AddRoleToUser(UserMatchRoleDTO userMatchRoleDTO)
        {
            return Ok(await _adminService.MatchingUserWtihRole(userMatchRoleDTO));
        }

    }
}
