using erbildaphneAPI.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace erbildaphneAPI.Entity.Services
{
    public interface IAccountService
    {
        Task<string> CreateUserAsync(RegisterDto model);
        Task<(string Message, List<string> Role)> FindByNameAsync(LoginDto model);

        Task<string> CreateRoleAsync(RoleDto model);

        Task<List<RoleDto>> GetAllRoles();

        Task<RoleDto> FindByIdAsync(int id);

        Task<UsersInOrOutDto> GetAllUsersWithRole(int id);

        Task<string> EditRoleListAsync(EditRoleDto model);
        Task DeleteRole(int id);
        Task<UserDto> Find(string username);

        string GenerateJwtToken(string email,List<string> role);
        Task LogoutAsync();
    }
}
