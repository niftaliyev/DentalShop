using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalShop.Models;
using DentalShop.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DentalShop.Services
{
    public class TokenGenerator
    {
        private readonly DentalShopDbContext context;

        public TokenGeneratorOptions Options { get; }

        public TokenGenerator(IOptions<TokenGeneratorOptions> options, DentalShopDbContext context)
        {
            Options = options.Value;
            this.context = context;
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateAccessToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Options.Secret);
            string role="User";
            var userRoles = context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
            if (userRoles != null)
            {
                role = context.Roles.Where(x => x.Id == userRoles.RoleId).FirstOrDefault().Name;

            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role.ToString()),


                }),
                Expires = DateTime.UtcNow.Add(Options.AccessExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}
