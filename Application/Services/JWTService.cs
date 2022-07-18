using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;


namespace Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly JWTSettings _jwtSettings;
        public JWTService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<IEnumerable<Claim>> DecodeJWT(JwtSecurityToken token) 
        {
            var claims = token.Claims;
            return claims;

        }

        public JwtSecurityToken GetToken(IList<Claim> userClaim, IList<string> roles, IdentityUser user)
        {
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaim)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
