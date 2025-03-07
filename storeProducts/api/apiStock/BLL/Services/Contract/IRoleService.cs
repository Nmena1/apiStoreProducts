using apiStock.DTO;

namespace apiStock.BLL.Services.Contract
{
    public interface IRoleService
    {
        public Task<List<roleDTO>> list();
    }
}
