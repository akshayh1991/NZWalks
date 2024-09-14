using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJwtToken(string username)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.UserData, username));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                _configuration["JWT:Issuer"],
                 _configuration["JWT:Audience"],
                 claims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
