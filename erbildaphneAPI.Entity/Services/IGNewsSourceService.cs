using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IGNewsSourceService
    {
        Task<IEnumerable<GNewsSourceDto>> GetAllAsync();
        Task<GNewsSourceDto> GetById(int id);
        Task Create(GNewsSourceDto model);

        void Update(GNewsSourceDto model);

        Task Delete(int id);
    }
}
