using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            UserName = httpContextAccessor.HttpContext.User?.FindFirstValue("sub");
        }

        public string UserId { get; }
        public string UserName { get; }
    }
}
