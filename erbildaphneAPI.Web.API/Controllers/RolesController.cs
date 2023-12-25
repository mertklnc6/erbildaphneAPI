using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using erbildaphneAPI.DataAccess.Identity.Models;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Rol oluştur
        [HttpPost("create")]
        //[Authorize]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Rol adı boş olamaz.");

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
                return BadRequest("Bu rol zaten var.");

            var result = await _roleManager.CreateAsync(new AppRole { Name = roleName });
            if (result.Succeeded)
                return Ok($"'{roleName}' adlı rol başarıyla oluşturuldu.");
            else
                return BadRequest("Rol oluşturulamadı.");
        }

        // Role kullanıcı atama
        [HttpPost("assign")]
        //[Authorize]
        public async Task<IActionResult> AssignRoleToUser(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return BadRequest("Kullanıcı bulunamadı.");

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                return BadRequest("Rol bulunamadı.");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
                return Ok($"'{userName}' kullanıcısına '{roleName}' rolü başarıyla atandı.");
            else
                return BadRequest("Rol ataması başarısız oldu.");
        }

        // Tüm rolleri listele
        [HttpGet("list")]
        //[Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}
