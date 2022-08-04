using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserInfoService
    {
        IJWTService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;

        public UserInfoService(IJWTService jWTService, UserManager<IdentityUser> userManager)
        {
            _jwtService = jWTService;
            _userManager = userManager;
        }

        public UserInfo SetUserInfo(UserInfoDTO dto, UserInfo userInfo, string token, string userId)
        {
           
            userInfo.UserID = userId;
            userInfo.Role = _jwtService.GetUserRole(token);
            userInfo.UserName = dto.UserName;
            userInfo.Surname = dto.Surname;
            userInfo.Name = dto.Name;
            userInfo.Email = dto.Email;
            userInfo.Gender = dto.Gender;
            userInfo.Contry = dto.Contry;
            userInfo.BirthDay = dto.BirthDay;
            userInfo.Address = dto.Address;
            userInfo.PhoneNumber = dto.PhoneNumber;
            userInfo.Age = dto.Age;
            return userInfo;
        }
    }
}
