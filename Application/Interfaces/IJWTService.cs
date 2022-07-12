using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        string GetJWT(string token);
        JwtSecurityToken GetToken(IList<Claim> userClaim, IList<string> roles, IdentityUser users);
        string DecodeToken(string token);


    }
}
