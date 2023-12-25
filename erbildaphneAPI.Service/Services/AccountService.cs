using AutoMapper;
using erbildaphneAPI.DataAccess.Identity.Models;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace erbildaphneAPI.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<string> CreateRoleAsync(RoleDto model)
        {
            string message = string.Empty;
            var role = new AppRole()
            {
                Name = model.Name,
                Description = model.Description
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                message = "OK";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    message = error.Description;
                }
                //ModelState.AddModelError("", "Rol kayıt işlemi gerçekleşmedi.");
            }
            return message;
        }
        public async Task<string> CreateUserAsync(RegisterDto model)
        {
            string message = string.Empty;
            var user = new AppUser()
            {
                Name = model.FirstName,
                Surname = model.LastName,
                Email = model.Email,
                UserName = model.Email  // Email adresini UserName olarak atayın
            };
            var identityResult = await _userManager.CreateAsync(user,model.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                message = "OK";
                return message;
            }

            var errors = string.Join("; ", identityResult.Errors.Select(e => e.Description));
            return errors;
        }



        public async Task<string> EditRoleListAsync(EditRoleDto model)
        {
            string msg = "OK";
            foreach (var userId in model.UserIdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        msg = $"{user.UserName} role eklenemedi";

                    }
                }
            }
            foreach (var userId in model.UserIdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        msg = $"{user.UserName} rolden çıkarılamadı";
                    }
                }

            }
            return msg;
        }
        public async Task<RoleDto> FindByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<RoleDto>(role);
        }
        public async Task<UserDto> Find(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _mapper.Map<UserDto>(user);
        }


        public async Task<(string Message, List<string> Role)> FindByNameAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return ("user not found", null);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!signInResult.Succeeded)
            {
                return ("invalid credentials", null);
            }

            // Kullanıcının rolünü al
            var roles = await _userManager.GetRolesAsync(user);
            List<string> role = new List<string>();
            foreach (var roleItem in roles)
            {
                role.Add(roleItem);
            }

            return ("OK", role);
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<UsersInOrOutDto> GetAllUsersWithRole(int id)
        {
            var role = await this.FindByIdAsync(id);

            var usersInRole = new List<AppUser>();
            var usersOutRole = new List<AppUser>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    usersInRole.Add(user);  //Bu rolde bulunan kullanıcıların listesi
                }
                else
                {
                    usersOutRole.Add(user); //Bu rolde olmayan kullanıcıların listesi
                }
            }
            UsersInOrOutDto model = new UsersInOrOutDto()
            {
                Role = _mapper.Map<RoleDto>(role),
                UsersInRole = _mapper.Map<List<UserDto>>(usersInRole),
                UsersOutRole = _mapper.Map<List<UserDto>>(usersOutRole)
            };
            return model;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public string GenerateJwtToken(string email, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
