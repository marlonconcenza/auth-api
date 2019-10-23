using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using auth_infra.Interfaces;
using auth_infra.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace auth_infra.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration _configuration { get; set; }

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string createToken(Account account) {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._configuration["AppSettings:Secret"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, account.id.ToString()),
                    new Claim(ClaimTypes.Role, account.role),
                    new Claim("permissions", account.permission)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
