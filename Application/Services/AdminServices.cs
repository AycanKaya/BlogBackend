using Application.DTO;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AdminServices : IAdminService
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IApplicationDbContext _context;
     
        public AdminServices(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IdentityUser[]> GetAllUsers()
        {
            var UserList = await _userManager.Users.ToArrayAsync();
            if (UserList == null)
                throw new ArgumentNullException(nameof(UserList));
            return UserList;
        }
        
      

        public async Task<Dictionary<string, IdentityUser[]>> GetUsersWithRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Dictionary<string, IdentityUser[]> hash = new Dictionary<string, IdentityUser[]>();
            foreach (var role in roles)
            {
                var userInRole = await _userManager.GetUsersInRoleAsync(role.ToString());
                hash.Add(role.ToString(), userInRole.ToArray());
            }
            return hash;
        }

        public async Task<bool> MatchingUserWtihRole(UserMatchRoleDTO userMatchRoleDTO)
        {           
            var user =   await _userManager.Users.Where(x => x.Id == userMatchRoleDTO.UserId).FirstOrDefaultAsync();
            
            if (user == null)
                throw new ArgumentNullException();

            var userInfo = await _context.UserInfo.Where(x => x.UserID == userMatchRoleDTO.UserId).FirstOrDefaultAsync();
            if (userInfo != null)
            {
                await _userManager.RemoveFromRoleAsync(user,userInfo.Role);
                userInfo.Role = userMatchRoleDTO.RoleName;
                await _userManager.AddToRoleAsync(user, userMatchRoleDTO.RoleName);
                await _context.SaveChanges();
                return true;
            }              
            var userInfoNew = new UserInfo();
            userInfoNew.UserID = userMatchRoleDTO.UserId;
            userInfoNew.Role = userMatchRoleDTO.RoleName;
            _context.UserInfo.Add(userInfoNew);
            await _userManager.AddToRoleAsync(user, userMatchRoleDTO.RoleName);
            await _context.SaveChanges();
            return true;

        }
        public async Task<IdentityRole> AddRole(string roleName)
        {
            var roles = await (_roleManager.Roles.Where(x => x.Name == roleName)).FirstOrDefaultAsync();
            if (roles != null)
                throw new Exception("This role already exist ");

            await _roleManager.CreateAsync(new IdentityRole(roleName));
            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == roleName);

            return role;

        }
        public async Task<UserInfo> SettingUserInfo(UserInfoDTO dto,string userId)
        {
            
            var existInfo =  _context.UserInfo.Where(x => x.UserID == userId).FirstOrDefault();

            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
                throw new ArgumentNullException();

            var role = await _userManager.GetRolesAsync(user);
            if(role != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role.ToString());
            }
           
            await _userManager.AddToRoleAsync(user, dto.Role);
            

            if (existInfo != null)
            {
                existInfo.UserName = dto.UserName;
                existInfo.Role = dto.Role;
                existInfo.Surname = dto.Surname;
                existInfo.Name = dto.Name;
                existInfo.Gender = dto.Gender;
                existInfo.Contry = dto.Contry;
                existInfo.BirthDay = dto.BirthDay;
                existInfo.Address = dto.Address;
                existInfo.PhoneNumber = dto.PhoneNumber;
                existInfo.Email = dto.Email;
                existInfo.Age = dto.Age;
                await _context.SaveChanges();             

                return existInfo;
            }
            var userInfo = new UserInfo();
            userInfo.UserID = userId;
            userInfo.Role = dto.Role;
            userInfo.UserName = dto.UserName;
            userInfo.Surname = dto.Surname;
            userInfo.Name = dto.Name;
            userInfo.Gender = dto.Gender;
            userInfo.Contry = dto.Contry;
            userInfo.BirthDay = dto.BirthDay;
            userInfo.Address = dto.Address;
            userInfo.Email = dto.Email;
            userInfo.PhoneNumber = dto.PhoneNumber;
            userInfo.Age = dto.Age;
            _context.UserInfo.Add(userInfo);
            await _context.SaveChanges();
            return userInfo;

        }

        public async Task<AccountLevel> AddAccountLevel(AccountLevelDTO accountLevelDTO)
        {
            var isContain = _context.AccountLevel.Where(c => c.Name == accountLevelDTO.Name).FirstOrDefault();
            if (isContain != null)
            {
                throw new Exception("Already exist!");
            }
            var accountLevel = new AccountLevel();
            accountLevel.Name = accountLevelDTO.Name;
            accountLevel.Level = accountLevelDTO.Level;
            _context.AccountLevel.Add(accountLevel);
            await _context.SaveChanges();
            return accountLevel;


        }

        public async Task<AccountLevel> AccountLevelUp(AccountLevelUpDTO dto)
        {
            var accountLevel = _context.AccountLevel.Where(c => c.Name == dto.LevelName).FirstOrDefault();
            var userAccountLevel = _context.UserAccountLevels.Where(c => c.UserID == dto.UserID).FirstOrDefault();
            if (userAccountLevel == null)
                throw new Exception("Account Level not found");

            userAccountLevel.AccountLevelID = accountLevel.Id;

            var userInfo= _context.UserInfo.Where(x => x.UserID == dto.UserID).FirstOrDefault();
            userInfo.Level = dto.LevelName;
            await _context.SaveChanges();
            return accountLevel;

        }



    }
}
