using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Application.Model;
using System.Text;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJWTService _jwtService;

        public AccountService(IConfiguration configuration, UserManager<IdentityUser> userManager,IJWTService service)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtService = service;
        }

        public async Task<ResponseModel> RegisterUser(RegisterModel model)
        {
            var exist_user = await _userManager.FindByEmailAsync(model.Email);
            if(exist_user != null)
                throw new Exception($"Username '{model.Username}' is already taken.");
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new Exception($"{result.Errors}");

            ResponseModel response = new ResponseModel();
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.Message = "User created successfully!";
            response.Status = "Success";
            return response;

        }
        public async Task<ResponseModel> LoginUser(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new Exception($"User not be found");

            if(await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token= _jwtService.GetToken(model.Username, userRoles);
                var handleToken = new JwtSecurityTokenHandler().WriteToken(token);
                if (token != null)
                {
                    return new ResponseModel()
                    {
                        UserName = model.Username,
                        Email=model.Email,
                        Message= "",
                        token=handleToken,
                    };

                }
                else
                {
                    throw new Exception("Null Token");
                }
                 
            }else
                throw new Exception($"Your password and your email address do not match.");


        }
    }
}
