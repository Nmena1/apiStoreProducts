using apiStock.BLL.Services;
using apiStock.BLL.Services.Contract;
using apiStock.DAL.Repository.Contract;
using apiStock.DTO;
using apiStock.Models;
using apiStock.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly _generalService _generycService;

        public usersController(IUserService userService, _generalService generalService)
        {
            _userService = userService;
            _generycService = generalService;
        }

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> create(userDTO model)
        {
            var resp = new Response<userDTO>();
            try
            {
                resp.status = true;
                model.CreateUser = "auto";/* _generycService.GetModelFromToken(User);*/
                resp.value = await _userService.add(model);
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
        public async Task<IActionResult> edit(userDTO model)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                model.CreateUser = _generycService.GetModelFromToken(User);
                resp.value = await _userService.edit(model);
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
                string user = _generycService.GetModelFromToken(User);
                resp.value = await _userService.delete(id,user);
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
