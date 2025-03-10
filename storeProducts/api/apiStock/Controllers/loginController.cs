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
    public class loginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly _generalService _generycService;

        public loginController(IUserService userService, _generalService generycService)
        {
            _userService = userService;
            _generycService = generycService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> create(loginDTO model)
        {
            var resp = new Response<sessionDTO>();
            try
            {
                resp.status = true;
                resp.value = await _userService.logIn(model);
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
