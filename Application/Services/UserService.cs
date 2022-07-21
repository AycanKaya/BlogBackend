using Application.Interfaces;
using Application.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IJWTService _jWTService;
        
        public UserService(IApplicationDbContext context, IJWTService jWTService)
        {
            _context = context;
            _jWTService = jWTService;
          
        }
        public string GetUserIdFromJWT(string token)
        {
            var jwt = _jWTService.GetTokenClaims(token);
            var userId = jwt.First(x => x.Type == "uid").Value;
            return userId;
        }
        public string GetUserName(string token)
        {
            var jwt = _jWTService.GetTokenClaims(token);
            var userName = jwt.First(x => x.Type == "sub").Value;
            return userName;
        }


    }
}
