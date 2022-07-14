using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Application.Model;
using Domain.Entities;
using Application.Interfaces;
using Application.DTO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTService _jwtService;
       
        public AccountService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IJWTService service)
        {
            
            _userManager = userManager;
            _jwtService = service;
            _signInManager = signInManager;
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
        public async Task<AuthenticationResponse> Login(AuthenticationRequest authenticationRequest, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(authenticationRequest.Email);
            if (user == null)
                throw new Exception($"User not be found");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, authenticationRequest.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception($"Invalid Credentials for '{authenticationRequest.Email}'.");
            }
            
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token= _jwtService.GetToken(userClaims,roles, user);


            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(token);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return response;



        }

      
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }










    }
}
