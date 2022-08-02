using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Application.DTO;
using Domain.Entities;
using Application.Wrappers;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityUser> MatchingUserWtihRole(UserMatchRoleDTO userMatchRoleDTO);
        Task<Dictionary<string, List<IdentityUser>>> GetUsersWithRole();
        Task<UserInfo> SettingUserInfo(UserInfoDTO dto, string userId);
        Task<IdentityRole> AddRole(string roleName);
        Task<BaseResponse<AccountLevel>> AddAccountLevel(AccountLevelDTO accountLevelDTO);
        Task<BaseResponse<AccountLevel>> AccountLevelUp(AccountLevelUpDTO dto);


    }
}
