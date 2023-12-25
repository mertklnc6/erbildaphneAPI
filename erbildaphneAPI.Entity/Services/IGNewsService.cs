using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IGNewsService
    {
        Task<IEnumerable<GNewsDto>> GetAllAsync();
        Task<GNewsDto> GetById(int id);
        Task Create(GNewsDto model);

        void Update(GNewsDto model);

        Task Delete(int id);
    }
}
