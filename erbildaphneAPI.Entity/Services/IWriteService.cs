
using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IWriteService
    {
        Task<IEnumerable<WriteDto>> GetAllAsync();
        Task<WriteDto> GetById(int id);
        Task Create(WriteDto model);

        void Update(WriteDto model);

        Task Delete(int id);




    }
}
