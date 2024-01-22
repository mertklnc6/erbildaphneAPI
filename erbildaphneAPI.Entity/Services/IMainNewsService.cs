
using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IMainNewsService
    {
        Task<IEnumerable<MainNewsDto>> GetAllAsync();
        Task<MainNewsDto> GetById(int id);
        Task Create(MainNewsDto model);

        void Update(MainNewsDto model);

        Task Delete(int id);




    }
}
