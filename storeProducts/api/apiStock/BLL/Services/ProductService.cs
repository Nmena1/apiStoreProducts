using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace apiStock.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenerycRepository<Product> _productService;
        private readonly IGenerycRepository<Category> _categoryService;
        private readonly IGenerycRepository<Supplier> _supplierService;
        private readonly IMapper _mapper;

        public ProductService(IGenerycRepository<Product> productService, 
            IGenerycRepository<Category> categoryService,
            IGenerycRepository<Supplier> supplierService,
            IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public async Task<List<productListDTO>> getList()
        {
            try
            {
                IQueryable<Product> tbproducts = await _productService.consult(mdl => mdl.Isactive == 1);
                IQueryable<Category> tbcategory = await _categoryService.consult(mdl => mdl.Isactive == 1);
                IQueryable<Supplier> tbsupplier = await _supplierService.consult(mdl => mdl.Isactive == 1);

                var list = (from pr in tbproducts
                            join ca in tbcategory on pr.CategoryId equals ca.Id
                            join su in tbsupplier on pr.SupplierId equals su.Id
                            select new productListDTO
                            {
                               Id = pr.Id,
                               Name = pr.Name,
                               Descripcion = pr.Descripcion,
                               Stock = pr.Stock,
                               Price = pr.Price,
                               categoryName = ca.Name,
                               CategoryId = pr.CategoryId,
                               SupplierId = pr.SupplierId,
                               supplierName = su.Name
                            }
                            );

                return list.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<productDTO> create(productDTO model)
        {
            try
            {
                var mdl = await _productService.get(mdl => mdl.Name == model.Name && mdl.Isactive == 1);
                if(mdl == null)
                {
                    throw new InvalidOperationException("Ya existe el producto ya existe");
                }

                var product = await _productService.create(_mapper.Map<Product>(model));
                if(product == null) { throw new TaskCanceledException("Error al guardar"); }

                return _mapper.Map<productDTO>(product);

            }catch { throw; }
        }

        public async Task<bool> edit(productDTO model)
        {
            try
            {
                var mdl = await _productService.get(mdl => mdl.Id == model.Id && mdl.Isactive == 1);
                if (mdl == null)
                {
                    throw new InvalidOperationException("No existe el producto ya existe");
                }

                mdl.ModifDate = DateTime.Now;
                mdl.ModifUser = model.ModifUser;
                mdl.Name = model.Name;
                mdl.Descripcion = model.Descripcion;
                mdl.Stock = model.Stock;
                mdl.Price = model.Price;

                if(!await _productService.update(mdl)) { throw new InvalidOperationException("Error al editar"); }

                return true;
            }
            catch { throw; }
        }

        public async Task<bool> delete(int id, string userEdit)
        {
            try
            {
                var mdl = await _productService.get(mdl => mdl.Id == id && mdl.Isactive == 1);
                if (mdl == null)
                {
                    throw new InvalidOperationException("No existe el producto");
                }

                mdl.ModifDate = DateTime.Now;
                mdl.ModifUser = userEdit;
                mdl.Isactive = 0;
                
                if (!await _productService.update(mdl)) { throw new InvalidOperationException("Error al eliminar"); }

                return true;
            }
            catch { throw; }
        }

    }
}
