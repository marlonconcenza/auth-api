using System;
using System.Collections.Generic;
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
            
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new Claim[] 
            //     {
            //         new Claim(ClaimTypes.Name, account.id.ToString()),
            //         new Claim(ClaimTypes.Role, account.role),
            //         new Claim("permissions", account.permissions != null ? account.permissions : string.Empty)
            //     }),
            //     Expires = DateTime.UtcNow.AddHours(4),
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Expires = DateTime.UtcNow.AddHours(4);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, account.id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, account.role));

            if (account.permissions != null) {
                foreach (var item in account.permissions) {
                    claims.Add(new Claim("permissions", item.permission));
                }
            }

            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
