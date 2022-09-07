using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO;
using Application.Wrappers;
using WebApi.Model;
using System.Net;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

     [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }
     

        [HttpGet("GetAllUsers")]
        public async Task<GetUsersResponseModel> GetAllUsers()
        {
            _logger.LogInformation("Hello from inside AdminController!");
            var users = await _adminService.GetAllUsers();
            var response = new GetUsersResponseModel(users,true,"All users here",200);
            return response;

        }

        [HttpGet]
        [Route("GetUserInRole")]
        public async Task<UsersInRoleResponseModel> GetUsersInRoles()
        {
            var userRole = await _adminService.GetUsersWithRole();
            return new UsersInRoleResponseModel(userRole, "Successfull", true, 200);
        }

        [HttpPost]
        [Route("NewUserRole")]
        public async Task<ResponseBase> AddNewRole(string RoleName)
        {
            var role = await _adminService.AddRole(RoleName);
            var response = new ResponseBase();
            response.setResponseMessage(true, role.Name + " added.", (int)HttpStatusCode.Created);
            return response;
        }


        [HttpPut("AddRoleToUser")]
        public async Task<ResponseBase> AddRoleToUser(UserMatchRoleDTO userMatchRoleDTO)
        {
            var status = await _adminService.MatchingUserWtihRole(userMatchRoleDTO);
            return new ResponseBase()
            {
                Succeeded = status,
                Message = userMatchRoleDTO.RoleName + " added to user.",
                StatusCode = (int)HttpStatusCode.OK
            };
        }



        [HttpPut("Dashboard")]
        public async Task<UserInfoResponseModel> SettingUsers(UserInfoDTO dto, string id)
        {
            var userInfo = await _adminService.SettingUserInfo(dto, id);
            return  new UserInfoResponseModel(userInfo, true, "User Info ready", 200);
           

        }


        [HttpPost]
        [Route("AddAccountLevel")]
        public async Task<ResponseBase> AddAccountLevel(AccountLevelDTO dto)
        {
            var accountLevel = await _adminService.AddAccountLevel(dto);
            var response = new ResponseBase();
            response.setResponseMessage(true, accountLevel.Name + " created successfully.", 200);
            return response;
        }


        [HttpPost]
        [Route("UserAccountLevelUP")]

        public async Task<ResponseBase> AccountLevelUp(AccountLevelUpDTO levelUpDTO)
        {
            var accountLevel = await _adminService.AccountLevelUp(levelUpDTO);
           
            var response = new ResponseBase();
            response.setResponseMessage(true, "User Level Updated to " + accountLevel.Name, 200);
            return response;
        }

    }
}
