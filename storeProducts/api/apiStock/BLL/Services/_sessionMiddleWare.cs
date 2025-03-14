using apiStock.DAL.Repository.Contract;
using apiStock.Models;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace apiStock.BLL.Services
{
    public class _sessionMiddleWare
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        //private readonly IGenerycRepository<UserSession> _sessionService;

        public _sessionMiddleWare(IConfiguration configuration, 
            RequestDelegate next/*,*/
            //IGenerycRepository<UserSession> sessionService
            )
        {
            _configuration = configuration;
            _next = next;
            //_sessionService = sessionService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                try
                {
                    // Validar el token
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Audience"],
                        ValidateLifetime = true, // Validar la expiración del token
                        ClockSkew = TimeSpan.Zero // No permitir margen de tiempo para la expiración
                    }, out SecurityToken validatedToken);

                    // Si el token es válido, registrar en la base de datos
                    //var jwtToken = (JwtSecurityToken)validatedToken;
                    //var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Actor).Value;
                    //var name = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                    //var id = jwtToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

                }
                catch (SecurityTokenExpiredException)
                {
                    // Token expirado
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token expirado.");
                    return;
                }
                catch (Exception)
                {
                    // Token inválido
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token inválido.");
                    return;
                }
            }

            // Continuar con el siguiente middleware
            await _next(context);
        }

    }
}
