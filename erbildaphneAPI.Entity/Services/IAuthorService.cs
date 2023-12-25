using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetById(int id);
        Task Create(AuthorDto model);

        void Update(AuthorDto model);

        Task Delete(int id);





    }
}
