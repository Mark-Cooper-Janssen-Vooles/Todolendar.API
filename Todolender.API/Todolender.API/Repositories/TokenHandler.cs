using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration config;

        public TokenHandler(IConfiguration config)
        {
            this.config = config;
        }

        public Task<string> CreateTokenAsync(User user)
        {
            // create claims 
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.UserData, user.Id.ToString()));

            // loop into roles of users => we don't have roles?! 
            //user.Roles.ForEach((role) =>
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //});

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15), // 15 minutes till expiry
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
