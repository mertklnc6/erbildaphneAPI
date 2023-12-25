using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IRumorService
    {
        Task<IEnumerable<RumorDto>> GetAllAsync();
        Task<RumorDto> GetById(int id);
        Task Create(RumorDto model);

        void Update(RumorDto model);

        Task Delete(int id);
    }
}
