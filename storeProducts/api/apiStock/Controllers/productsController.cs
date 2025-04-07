using apiStock.BLL.Services;
using apiStock.BLL.Services.Contract;
using apiStock.DTO;
using apiStock.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly _generalService _generyService;

        public productsController(IProductService productService, _generalService generyService)
        {
           _productService = productService;
           _generyService = generyService;
        }

        [HttpGet]
        //[Authorize]
        [Route("list")]
        public async Task<IActionResult> Get()
        {
            var resp = new Response<List<productListDTO>>();
            try
            {
                resp.status = true;
                resp.value = await _productService.getList();
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return Ok(resp);

        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> create(productDTO model)
        {
            var resp = new Response<productDTO>();
            try
            {
                resp.status = true;
                model.CreateUser = _generyService.GetModelFromToken(User);
                resp.value = await _productService.create(model);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return Ok(resp);

        }

        [HttpPut]
        [Authorize]
        [Route("edit")]
        public async Task<IActionResult> edit(productDTO model)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                model.CreateUser = _generyService.GetModelFromToken(User);
                resp.value = await _productService.edit(model);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return Ok(resp);

        }

        [HttpDelete]
        [Authorize]
        [Route("delete")]
        public async Task<IActionResult> detele(int id)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                string user = _generyService.GetModelFromToken(User);
                resp.value = await _productService.delete(id, user);
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return Ok(resp);

        }
    }
}
