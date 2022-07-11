using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Application.Model;
using Domain.Entities;
using Application.Interfaces;
using Application.DTO;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJWTService _jwtService;

        public AccountService(UserManager<IdentityUser> userManager,IJWTService service)
        {
            
            _userManager = userManager;
            _jwtService = service;
        }

  

        public async Task<ResponseModel> Register(RegisterRequest registerRequest)
        {
            var exist_user = await _userManager.FindByEmailAsync(registerRequest.Email);
            if(exist_user != null)
                throw new Exception($"Username '{registerRequest.Username}' is already taken.");
            IdentityUser user = new()
            {
                Email = registerRequest.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerRequest.Username

            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                throw new Exception($"{result.Errors}");

            ResponseModel response = new ResponseModel();
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.Message = "User created successfully!";
            response.Status = "Success";
            return response;

        }
        public async Task<ResponseModel> Login(AuthenticationRequest authenticationRequest)
        {
            var user = await _userManager.FindByEmailAsync(authenticationRequest.Email);
            if (user == null)
                throw new Exception($"User not be found");

            if(await _userManager.CheckPasswordAsync(user, authenticationRequest.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token= _jwtService.GetToken(authenticationRequest.Email, userRoles);
                var handleToken = new JwtSecurityTokenHandler().WriteToken(token);
                if (token != null)
                {
                    return new ResponseModel()
                    {
                      
                        Email=authenticationRequest.Email,
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
