 using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Application.Model;
using Domain.Entities;
using Application.Interfaces;
using Application.DTO;
using System.Security.Cryptography;
using Application.Wrappers;
using System.Net.Mail;
using Domain.Enum;
namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTService _jwtService;
        private readonly IApplicationDbContext _context;
       
        public AccountService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IJWTService service, IApplicationDbContext context)
        {
            
            _userManager = userManager;
            _jwtService = service;
            _signInManager = signInManager;
            _context = context;
        }

        private bool isValidEmail(string email)
        {
            
                var trimmedEmail = email.Trim();

                if (trimmedEmail.EndsWith("."))
                {
                    return false; 
                }
                try
                {
                    var emailAddress = new MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }

        private bool isValidatePassword(string passWord)
        {
            if (string.IsNullOrEmpty(passWord) || passWord.Length < 8)
                return false;
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 2) return false;
            return true;
        }


        public async Task<BaseResponse<string>> Register(RegisterRequest registerRequest)
        {
            var exist_user = await _userManager.FindByEmailAsync(registerRequest.Email);
            if(exist_user != null)
                throw new Exception($"Username '{registerRequest.Username}' is already taken.");
            if (isValidEmail(registerRequest.Email) && isValidatePassword(registerRequest.Password))
            {


                IdentityUser user = new()
                {
                    Email = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerRequest.Username

                };
                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                    throw new Exception($"{result.Errors}");
                return new BaseResponse<string>(user.UserName, result.ToString());
            }
            else
                 throw new ExceptionResponse("Invalid email or passwprd !");

            

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

        public  bool isLogged(string token)
        {
            var validateToken =  _jwtService.ValidateToken(token);
            if (validateToken == null)
                return false;
            return true;
        }

        public async Task<BaseResponse<UserInfo>> SettingUserInfo(UserInfoDTO dto, string token)
        {
            var userId = _jwtService.GetUserIdFromJWT(token);
            var existInfo = _context.UserInfo.Where(x => x.UserID == userId).FirstOrDefault();
            if(existInfo == null)
            {
                var userInfo = new UserInfo();
                userInfo.UserID = userId;
                userInfo.Role = _jwtService.GetUserRole(token);
                userInfo.UserName = dto.UserName;
                userInfo.Surname = dto.Surname;
                userInfo.Name = dto.Name;
                userInfo.Gender = dto.Gender;
                userInfo.Contry = dto.Contry;
                userInfo.BirthDay = dto.BirthDay;
                userInfo.Address = dto.Address;
                userInfo.PhoneNumber = dto.PhoneNumber; 
                userInfo.Age = dto.Age;
                _context.UserInfo.Add(userInfo);
                await _context.SaveChanges();
                return new BaseResponse<UserInfo>(userInfo);
            }
                existInfo.UserName = dto.UserName;
                existInfo.Surname = dto.Surname;
                existInfo.Name = dto.Name;
                existInfo.Gender = dto.Gender;
                existInfo.Contry = dto.Contry;
                existInfo.BirthDay = dto.BirthDay;
                existInfo.Address = dto.Address;
                existInfo.PhoneNumber = dto.PhoneNumber;
                existInfo.Age = dto.Age;
                await _context.SaveChanges();
                return new BaseResponse<UserInfo>(existInfo);

            


        }
        public async Task<BaseResponse<UserInfo>> GetUserInfoAsync(string token)
        {
            var userId = _jwtService.GetUserIdFromJWT(token);
            var userInfo = _context.UserInfo.Where(x => x.UserID == userId).FirstOrDefault();
            if (userInfo == null)
                throw new ExceptionResponse("User Not Found");
            return new BaseResponse<UserInfo>(userInfo);


        }





    }
}
