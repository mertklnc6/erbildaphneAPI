using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    
    [ApiController]    
    [Route("api/[controller]")]
    [EnableCors("MyCorsePolicy")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IAccountService _service;

        public RoleController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("get/")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllRoles();
            return Ok(list);
        }


        [HttpPost("create/")]
        public async Task<IActionResult> Create([FromBody] RoleDto model)
        {
            string msg = await _service.CreateRoleAsync(model);

            if (msg == "OK")
            {
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", msg);
            }

            return Ok(model);
        }

        [HttpGet("user/{id}")]

        public async Task<IActionResult> Edit(int id)
        {
            var list = await _service.GetAllUsersWithRole(id);

            return Ok(list);
        }


        [HttpPut("edit/")]
        public async Task<IActionResult> Edit([FromBody] EditRoleDto model)
        {
            string msg = await _service.EditRoleListAsync(model);
            if (msg != "OK")
            {
                ModelState.AddModelError("", msg);
                return Ok(model);
            }

            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var item = _service.FindByIdAsync(id);

            if (item != null)
            {
                await _service.DeleteRole(id);
                return Ok();
            }
            return BadRequest();
        }


    }
}

