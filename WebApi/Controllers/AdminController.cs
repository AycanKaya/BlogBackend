using System.Net;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Model;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

     [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        IAdminService _adminService;
        private readonly ILogger _logger;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            _logger = Serilog.Log.ForContext<AdminController>();
        }
     

        [HttpGet("GetAllUsers")]
        public async Task<GetUsersResponseModel> GetAllUsers()
        {
            _logger.Information("Deneme");
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
