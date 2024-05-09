using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;
using Microsoft.IdentityModel.Tokens;

namespace GenericSmallBusinessApp.Server.AuthenticationAndAuthorization
{
    public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
    {
        private string CreateJwt(User user)
        {
            var roleName = RoleIdentity.RoleName(user.RoleId);

            List<Claim> claims = new() 
            {
                new Claim(ClaimTypes.Name, user.EmailAddress),
                new Claim(ClaimTypes.Role, roleName)
            };

            var issuer = configuration.GetSection("JwtSettings:Issuer").Value;
            var audience = configuration.GetSection("JwtSettings:Audience").Value;
            var credentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration.GetSection("JwtSettings:Key").Value)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string GetJwt(User user)
        {
            return CreateJwt(user);
        }
    }
}
