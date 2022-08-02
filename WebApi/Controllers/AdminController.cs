﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("NewUserRole")]
        public async Task<IActionResult> AddNewRole(string RoleName)
        {
            return Ok(await _adminService.AddRole(RoleName));
        }

         
         [HttpPut("AddRoleToUser")]
         public async Task<IActionResult> AddRoleToUser(UserMatchRoleDTO userMatchRoleDTO)
        {
            return Ok(await _adminService.MatchingUserWtihRole(userMatchRoleDTO));
        }
         [HttpPut("Dashboard")]
         public async Task<IActionResult> SettingUsers(UserInfoDTO userInfo,string id)
        {
            return Ok(await _adminService.SettingUserInfo(userInfo, id));

        }


        [HttpPost]
        [Route("AddAccountLevel")]
        public async Task<IActionResult> AddAccountLevel(AccountLevelDTO dto)
        {
            return Ok(await _adminService.AddAccountLevel(dto));
        }


        [HttpPost]
        [Route("UserAccountLevelUP")]

        public async Task<IActionResult> AccountLevelUp(AccountLevelUpDTO levelUpDTO)
        {
            return Ok(await _adminService.AccountLevelUp(levelUpDTO));
        }

    }
}
