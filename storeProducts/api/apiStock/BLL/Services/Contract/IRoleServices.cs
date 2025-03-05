using apiStock.DTO;

namespace apiStock.BLL.Services.Contract
{
    public interface IRoleServices
    {
        public Task<List<roleDTO>> list();
    }
}
