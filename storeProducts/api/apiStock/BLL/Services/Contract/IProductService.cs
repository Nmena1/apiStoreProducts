using apiStock.DTO;

namespace apiStock.BLL.Services.Contract
{
    public interface IProductService
    {
       public Task<List<productListDTO>> getList();
       
       public Task<productDTO> create(productDTO model);
       
       public Task<bool> edit(productDTO model);
       
       public Task<bool> delete(int id, string userEdit);
    }
}
