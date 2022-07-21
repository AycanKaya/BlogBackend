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

        
    }
}
