using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using AutoMapper;

namespace apiStock.BLL.Services
{
    public class MovementService : IMovementService
    {
        private readonly IGenerycRepository<StockMovement> _movementServices;
        private readonly IGenerycRepository<Product> _productServices;
        private readonly IGenerycRepository<User> _userServices;
        private readonly IMapper _mapper;

        public MovementService(IGenerycRepository<StockMovement> movementServices, 
            IGenerycRepository<Product> productServices,
            IGenerycRepository<User> userServices,
            IMapper mapper)
        {
            _movementServices = movementServices;
            _productServices = productServices;
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<List<movementListDTO>> getList()
        {
            try
            {
                IQueryable<Product> tbproducts = await _productServices.consult(mdl => mdl.Isactive == 1);
                IQueryable<StockMovement> tbmovement = await _movementServices.consult(mdl => mdl.Isactive == 1);
                IQueryable<User> tbuser = await _userServices.consult(mdl => mdl.Isactive == 1);

                var list = (from mv in tbmovement
                            join pr in tbproducts on mv.ProductId equals pr.Id
                            join us in tbuser on mv.UserId equals us.Id
                            orderby mv.CreateDate
                            select new movementListDTO
                            {
                                Id = mv.Id,
                                ProductId = mv.ProductId,
                                Type = mv.Type,
                                Descripcion = mv.Descripcion,
                                Cuantity = mv.Cuantity.Value,
                                UserId = mv.UserId,
                                Description = mv.Description,
                                productName = pr.Name,
                                userName = us.Name
                            }
                            );

                return list.ToList();
            }
            catch { throw; }
        }

        public async Task<movementDTO> add(movementDTO model)
        {
            try
            {
                var product = await _productServices.get(mdl => mdl.Id == model.ProductId && mdl.Isactive == 1);
                if(product == null)
                {
                    throw new InvalidOperationException("El registro no existe");
                }

                if(product.Stock < model.Cuantity || product.Stock == 0){ throw new InvalidOperationException("No hay suficiente inventario"); }

                var mdl = await _movementServices.create(_mapper.Map<StockMovement>(model));
                if(mdl == null) { throw new InvalidOperationException("Error al guardar"); }

                editProduct(mdl.Id, Convert.ToInt32(model.Cuantity), model.ModifUser);

                return _mapper.Map<movementDTO>(mdl);

            }
            catch { throw; }
        }

        public async Task<bool> update(movementDTO model)
        {
            try
            {
                var mdl = await _movementServices.get(mdl => mdl.Id == model.Id && mdl.Isactive == 1);
                if (mdl == null)
                {
                    throw new InvalidOperationException("El registro no existe");
                }

                mdl.ProductId = model.ProductId;
                mdl.Type = model.Type;
                mdl.Descripcion = model.Descripcion;
                mdl.Cuantity = model.Cuantity;
                mdl.UserId = model.UserId;
                
                mdl.ModifDate = DateTime.Now;
                mdl.ModifUser = model.ModifUser;

                if(!await _movementServices.update(mdl)) { throw new InvalidOperationException("Error al editar"); }

                return true;
            }


            catch { throw; }
        }

        private async Task<bool> editProduct(int id, int quantity, string userEdit)
        {
            var mdl = await _productServices.get(mdl => mdl.Id == id && mdl.Isactive == 1);
            if(mdl == null) { throw new InvalidOperationException("El registro no existe"); }
            
            if(mdl.Stock <= quantity)
            {
                mdl.Stock = quantity;
                mdl.ModifUser = userEdit;
                mdl.ModifDate = DateTime.Now;

                return (await _productServices.update(mdl));
            }else { return false; }

        }
        
    }
}
