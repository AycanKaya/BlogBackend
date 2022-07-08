﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        string GetJWT(string token);
        JwtSecurityToken GetToken(string userName, IList<string> roles);
        string DecodeToken(string token);


    }
}
