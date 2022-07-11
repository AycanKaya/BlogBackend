using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AdminServices : IAdminService
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
     
        public AdminServices(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            var UserList = await _userManager.Users.ToListAsync();
            if (UserList == null)
                throw new ArgumentNullException(nameof(UserList));
            return UserList;
        }
        public async Task<Dictionary<string, List<IdentityUser>>> GetUsersWithRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Dictionary<string, List<IdentityUser>> hash = new Dictionary<string, List<IdentityUser>>();
            foreach (var role in roles)
            {
                var userInRole = await _userManager.GetUsersInRoleAsync(role.ToString());
                hash.Add(role.ToString(), (List<IdentityUser>)userInRole);
            }
            return hash;
        }

        public async Task<IdentityUser> MatchingUserWtihRole(UserMatchRoleDTO userMatchRoleDTO)
        {           
            var user =   await _userManager.Users.Where(x => x.Id == userMatchRoleDTO.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new ArgumentNullException();
            await _userManager.AddToRoleAsync(user, userMatchRoleDTO.RoleName);
            return user;

        }

    }
}
