using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace apiStock.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenerycRepository<User> _userService;
        private readonly IMapper _mapper;
        private readonly _encriptService _encriptService;
        private readonly _generateTKN _tokenService;
        private readonly _generalService _generalService;

        public UserService(IGenerycRepository<User> userService, IMapper mapper, _encriptService encriptService, _generateTKN tokenService, _generalService generalService)
        {
            _userService = userService;
            _mapper = mapper;
            _encriptService = encriptService;  
            _tokenService = tokenService;
            _generalService = generalService;
        }

        public async  Task<List<userListDTO>> list()
        {
            try
            {
                var mdl = await _userService.consult(mdl => mdl.Isactive == 1);
                return _mapper.Map<List<userListDTO>>(mdl.ToList());
            }
            catch { throw; }
        }

        public async Task<userDTO> add(userDTO model)
        {
            try
            {
                var mdl = await _userService.get(mdl => mdl.UserName == model.UserName && mdl.Isactive == 1);
                if(mdl != null)
                {
                    throw new TaskCanceledException("Usuario ya existe...");
                }
                
                string _encriptPass = _encriptService.HashPassword(model.Password);
                model.Password = _encriptPass;
                var userMdl = await _userService.create(_mapper.Map<User>(model));
                if(userMdl == null) { throw new TaskCanceledException("No se pudo crear"); }

                return _mapper.Map<userDTO>(userMdl);

            }
            catch { throw; }
        }

        public async Task<bool> edit(userDTO model)
        {
            try
            {
                var mdl = await _userService.get(mdl => mdl.Id == model.Id && mdl.Isactive == 1);

                if(mdl==null) { throw new TaskCanceledException("El usuario no existe"); }

                mdl.ModifDate = DateTime.Now;
                mdl.Name = model.Name;
                mdl.RolId = model.RolId;

                if(!await _userService.update(mdl))
                {
                    throw new TaskCanceledException("Error al editar");
                }
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> delete(int id, string editUser)
        {
            try
            {
                var mdl = await _userService.get(mdl => mdl.Id == id && mdl.Isactive == 1);

                if (mdl == null) { throw new TaskCanceledException("El usuario no existe"); }

                mdl.ModifDate = DateTime.Now;
                mdl.Isactive = 0;
                mdl.ModifUser = editUser;

                if (!await _userService.update(mdl))
                {
                    throw new TaskCanceledException("Error al eliminar");
                }
                return true;
            }
            catch { throw; }
        }


        public async Task<sessionDTO> logIn(loginDTO model)
        {
            try
            {
                var mdl = await _userService.get(mdl => mdl.UserName == model.UserName && mdl.Isactive == 1);

                if (mdl == null) { throw new TaskCanceledException("El usuario no existe"); }

                if(!_encriptService.VerifyPassword(model.Password, mdl.Password))
                {
                    throw new TaskCanceledException("Contraseña incorrecta..");
                }

                sessionDTO session = new sessionDTO
                {
                    user = mdl.UserName,
                    name = mdl.Name,
                    rol = mdl.RolId.ToString(),
                    token = _tokenService.GenerateToken(mdl)
                };

                //bool resp = await _generalService.registerSession(mdl.Id, ParseJwtToken(session.token), session.name, session.user);
                bool resp = await _generalService.registerSession(mdl.Id, session.token, session.name, session.user);

                return session;
            }
            catch { throw; }
        }


        public static JwtSecurityToken ParseJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("El token no puede estar vacío.");
            }

            // Crear un manejador de tokens JWT
            var handler = new JwtSecurityTokenHandler();

            // Validar si el token es un JWT válido
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("El token no es un JWT válido.");
            }

            // Parsear el token en un objeto JwtSecurityToken
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken;
        }
    }
}
