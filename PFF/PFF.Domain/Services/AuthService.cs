using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PFF.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PFF.Shared.Dtos;

namespace PFF.Domain.Services
{
    public class AuthService : IAuthRepository
    {
        private readonly string _secretKey;

        public AuthService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
        }

        public string Authenticate(LoginDto loginDto)
        {
            // Vérifier les informations d'identification dans la base de données
            // Pour cet exemple, nous supposons que l'authentification est réussie
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, loginDto.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
