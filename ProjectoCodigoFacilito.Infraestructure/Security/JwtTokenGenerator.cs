using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectoCodigoFacilito.Application.GetTokenResult;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectoCodigoFacilito.Infraestructure.Security
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwt(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                new Claim(JwtClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public GetTokenResult GetValuesFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);
            if (userIdClaim == null)
            {
                return null;
            }

            // Leer la fecha de expiración del token en formato UTC
            var expirationTimeUtc = token.ValidTo;

            // Obtener información de la zona horaria UTC-3
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");


            // Convertir la fecha y hora de expiración a la zona horaria UTC-3
            var expirationTimeUtcMinus3 = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, timeZone);

            // Retornar el resultado con la fecha y hora en la zona horaria UTC-3
            return new GetTokenResult { Id = userIdClaim.Value, ExpirationToken = expirationTimeUtcMinus3 };
        }
    }
}
