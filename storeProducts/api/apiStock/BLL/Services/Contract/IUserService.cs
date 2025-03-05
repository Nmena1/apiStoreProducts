using apiStock.DTO;

namespace apiStock.BLL.Services.Contract
{
    public interface IUserService
    {
        public Task<List<userListDTO>> list();

        public Task<userDTO> add(userDTO model);

        public Task<bool> edit(userDTO model);

        public Task<bool> delete(int id);

        public Task<sessionDTO> logIn(loginDTO model);


    }
}
