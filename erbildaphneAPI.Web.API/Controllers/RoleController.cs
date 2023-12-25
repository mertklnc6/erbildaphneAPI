using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class RoleController : ControllerBase
    {
        private readonly IAccountService _service;

        public RoleController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllRoles();
            return Ok(list);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(RoleDto model)
        {
            string msg = await _service.CreateRoleAsync(model);

            if (msg == "OK")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", msg);
            }

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var list = await _service.GetAllUsersWithRole(id);

            return Ok(list);
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditRoleDto model)
        {
            string msg = await _service.EditRoleListAsync(model);
            if (msg != "OK")
            {
                ModelState.AddModelError("", msg);
                return Ok(model);
            }

            return Ok();
        }


    }
}

