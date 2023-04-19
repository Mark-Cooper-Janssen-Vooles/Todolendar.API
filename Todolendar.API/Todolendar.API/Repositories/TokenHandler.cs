using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todolendar.API.Models.Domain;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment _env;

        public TokenHandler(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this._env = env;
        }

        public async Task<string> FetchJwtKey()
        {
            var env = _env.EnvironmentName;
            Console.WriteLine(env);
            string jwtKey = "";
            if (env == "Production")
            {
                jwtKey = await GetAWSSecret.GetSecret("prod/Todolendar/Jwt", "jwt");
            }
            else
            {
                jwtKey = config["Jwt:Key"];
            }

            return jwtKey;
        } 

        public async Task<string> CreateTokenAsync(User user)
        {
            // create claims 
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.UserData, user.Id.ToString()));

            string jwtKey = await FetchJwtKey();

            Console.WriteLine(jwtKey);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(720), // 720 (12hrs) minutes till expiry
                signingCredentials: credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
