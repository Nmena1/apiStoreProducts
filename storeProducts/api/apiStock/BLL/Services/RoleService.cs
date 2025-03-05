using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using AutoMapper;


namespace apiStock.BLL.Services
{
    public class RoleService : DAL.Repository.Contract.IRoleServices
    {
        private readonly IGenerycRepository<Role> _rolService;
        private readonly IMapper _mapper;

        public RoleService(IGenerycRepository<Role> rolService, IMapper mapper)
        {
            _rolService = rolService;
            _mapper = mapper;
        }

        public async Task<List<roleDTO>> list()
        {
            try
            {
                var list = await _rolService.consult();
                return _mapper.Map<List<roleDTO>>(list.ToList());
            }
            catch { throw; }
        }
    }
}
