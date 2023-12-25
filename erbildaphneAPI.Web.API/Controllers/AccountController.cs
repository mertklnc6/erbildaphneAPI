
using erbildaphneAPI.DataAccess.Identity.Models;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            string msg = await _accountService.CreateUserAsync(model);
            if (msg == "OK")
            {


                return Ok();
            }
            else
            {
                return BadRequest(msg);
            }

        }



        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto model)
        {
            (string message, List<string> role) = await _accountService.FindByNameAsync(model);
            if (role != null)
            {
                foreach (var roleClaim in role)
                {
                    if (roleClaim == "Admin")
                    {
                        if (message == "OK")
                        {
                            var token = _accountService.GenerateJwtToken(model.Email, roleClaim);
                            return Ok(new { Token = token });
                        }
                    }
                    else if (roleClaim == "Editor")
                    {
                        if (message == "OK")
                        {
                            var token = _accountService.GenerateJwtToken(model.Email, roleClaim);
                            return Ok(new { Token = token });
                        }
                    }
                }
            }

            return Unauthorized();
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok(new { Message = "Logout successful" });
        }


    }


}



