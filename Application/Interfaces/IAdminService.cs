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
        Task<IdentityUser[]> GetAllUsers();
        Task<bool> MatchingUserWtihRole(UserMatchRoleDTO userMatchRoleDTO);
        Task<Dictionary<string, IdentityUser[]>> GetUsersWithRole();
        Task<UserInfo> SettingUserInfo(UserInfoDTO dto, string userId);
        Task<IdentityRole> AddRole(string roleName);
        Task<AccountLevel> AddAccountLevel(AccountLevelDTO accountLevelDTO);
        Task<AccountLevel> AccountLevelUp(AccountLevelUpDTO dto);


    }
}
