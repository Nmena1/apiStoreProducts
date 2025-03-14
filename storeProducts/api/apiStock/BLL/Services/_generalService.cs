using apiStock.DAL.Repository.Contract;
using apiStock.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiStock.BLL.Services
{
    public class _generalService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenerycRepository<UserSession> _sessionService;

        public _generalService(IConfiguration configuration, IGenerycRepository<UserSession> sessionService)
        {
            _configuration = configuration;
            _sessionService = sessionService;
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

        public async Task<bool> registerSession(int id, string token, string name, string user)
        {
            try
            {
                var list = await _sessionService.consult(mdl => mdl.UserId == id && mdl.Isactive == 1);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        item.Isactive = 0;
                        item.ModifUser = "auto";
                        item.ModifDate = DateTime.Now;
                        item.EndTime = DateTime.Now;
                        bool resp = await _sessionService.update(item);
                    }
                }

                UserSession session = new UserSession
                {
                    UserId = id,
                    Token = token.ToString(),
                    Ipaddress = null,
                    UserAgent = name,
                    CreateUser = user
                };

                var mdl = await _sessionService.create(session);
                if (mdl == null)
                {
                    throw new TaskCanceledException("No se pudo crear la sesión.");
                }

                return true;
            }
            catch { throw; }
        }
    }
}
