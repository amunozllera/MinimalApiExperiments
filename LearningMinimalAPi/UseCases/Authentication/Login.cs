using MinimalApi.Models.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApi.UseCases.Authentication
{
    public static class Login
    {
        public static async Task<Results<Ok<string>, ForbidHttpResult>> Handle(LoginDto data, IConfiguration configuration, Serilog.ILogger logger)
        {
            if(data.Email == "a" && data.Password == "a")
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", "1"),
                        new Claim(JwtRegisteredClaimNames.Sub, data.Email),
                        new Claim(JwtRegisteredClaimNames.Email, data.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    Audience = audience,
                    Issuer = issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                var jwtToken = jwtTokenHandler.WriteToken(token);

                return TypedResults.Ok(jwtToken);
            }
            return TypedResults.Forbid();
        }
    }
}
