using apiStock.BLL.Services.Contract;
using apiStock.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiStock.DTO;
using Microsoft.AspNetCore.Authorization;
using apiStock.Response;

namespace apiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class movementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        private readonly _generalService _generycService;

        public movementController(IMovementService movementService, _generalService generycService)
        {
            _movementService = movementService;
            _generycService = generycService;
        }

        [HttpGet]
        [Authorize]
        [Route("list")]
        public async Task<IActionResult> Get()
        {
            var resp = new Response<List<movementListDTO>>();
            try
            {
                resp.status = true;
                resp.value = await _movementService.getList();
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
        public async Task<IActionResult> create(movementDTO model)
        {
            var resp = new Response<movementDTO>();
            try
            {
                resp.status = true;
                model.CreateUser = _generycService.GetModelFromToken(User);
                resp.value = await _movementService.add(model);
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
        public async Task<IActionResult> edit(movementDTO model)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                model.CreateUser = _generycService.GetModelFromToken(User);
                resp.value = await _movementService.update(model);
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
