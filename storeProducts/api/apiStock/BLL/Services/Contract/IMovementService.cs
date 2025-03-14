using apiStock.DTO;

namespace apiStock.BLL.Services.Contract
{
    public interface IMovementService
    {
        public Task<List<movementListDTO>> getList();

        public Task<movementDTO> add(movementDTO movementDTO);

        public Task<bool> update(movementDTO movementDTO);
    }
}
