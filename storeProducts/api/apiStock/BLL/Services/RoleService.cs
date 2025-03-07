using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using AutoMapper;

namespace apiStock.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenerycRepository<Role> _rolService;
        private readonly IMapper _mapper;

        public RoleService(IGenerycRepository<Role> rolService)
        {
            _rolService = rolService;
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
