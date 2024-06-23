using agence_bancaire_Business_Layer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace agence_bancaire_API.Repositories
{
    public class tokenRepository : ItokenReposirtoy
    {
        private readonly IConfiguration configuration;
        public tokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWTToken(clsUser User, string Role)
        {
            // Create Claims

            var claims = new List<Claim>();

            claims.Add( new Claim(ClaimTypes.Email , User.PersonInfo.Email)    );
            claims.Add( new Claim(ClaimTypes.Role , Role)    );

            var key = new  SymmetricSecurityKey (Encoding.UTF8.GetBytes(configuration["JWT:KEY"] ))  ;
            var credential = new  SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"], configuration["JWT:Audiance"], claims,
                expires: DateTime.Now.AddMinutes(15) , signingCredentials: credential
                ); 

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
