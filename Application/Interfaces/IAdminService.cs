using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Application.DTO;
namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityUser> MatchingUserWtihRole(UserMatchRoleDTO userMatchRoleDTO);
        Task<Dictionary<string, List<IdentityUser>>> GetUsersWithRole();
    }
}
