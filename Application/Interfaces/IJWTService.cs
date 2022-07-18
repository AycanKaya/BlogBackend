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
        JwtSecurityToken GetToken(IList<Claim> userClaim, IList<string> roles, IdentityUser users);
        Task<IEnumerable<Claim>> DecodeJWT(JwtSecurityToken token);


    }
}
