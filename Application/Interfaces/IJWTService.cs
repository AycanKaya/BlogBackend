﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        JwtSecurityToken GetToken(IList<Claim> userClaim, IList<string> roles, IdentityUser users);
        JwtSecurityToken? ValidateToken(string token);
        IEnumerable<Claim> GetTokenClaims(string tokenStr);
        string GetUserIdFromJWT(string token);
        string GetUserName(string token);
        string GetUserRole(string token);


    }
}
