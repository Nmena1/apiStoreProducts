using System.Security.Claims;

namespace apiStock.BLL.Services
{
    public class _generalService
    {
        private readonly IConfiguration _configuration;

        public _generalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetModelFromToken(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "Sid");
            var userName = user.Claims.FirstOrDefault(c => c.Type == "Name");
            var name = user.Claims.FirstOrDefault(c => c.Type == "Actor");
            var rolId = user.Claims.FirstOrDefault(c => c.Type == "Role");

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("El token no contiene un claim 'Estructura'.");
            }

            return userName.Value;
        }
    }
}
