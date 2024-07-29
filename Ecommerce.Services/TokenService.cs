using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Configuration;
using Ecommerce.Core.IRepositories.IServices;
using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class TokenService : ITokenService
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly UserManager<LocalUser> userManager;
        private readonly string secretkey;
        public TokenService(Microsoft.Extensions.Configuration.IConfiguration configuration,
            UserManager<LocalUser> userManager)    
        {
            this.configuration = configuration;
            this.userManager = userManager;
            secretkey = configuration.GetSection("TokenSetting")["SecretKey"];

        }

        public async Task<string> CreateTokenAsync(LocalUser localUser)
        {
            var key = Encoding.ASCII.GetBytes(secretkey);

            var roles = await userManager.GetRolesAsync(localUser);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, localUser.FirstName),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescription);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
    }
}
