using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MS.Application.Authorization.Common.Interfaces;
using MS.Domain.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Services
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(User userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Secret").Value!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Name, userInfo.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration.GetSection("Jwt:ExpireHours").Value!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
