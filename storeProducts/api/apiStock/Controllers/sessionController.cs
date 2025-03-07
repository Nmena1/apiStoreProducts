using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using apiStock.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sessionController : ControllerBase
    {
        private readonly IUserService _userService;

        public sessionController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get()
        {
            var resp = new Response<List<userListDTO>>();
            try
            {
                resp.status = true;
                resp.value = await _userService.list();
            }
            catch(Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return Ok(resp);

        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> create(userDTO model)
        {
            var resp = new Response<userDTO>();
            try
            {
                resp.status = true;
                model.CreateUser = "auto";
                resp.value = await _userService.add(model);
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
