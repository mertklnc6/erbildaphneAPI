
using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface ISecondArticleService
    {
        Task<IEnumerable<SecondArticleDto>> GetAllAsync();
        Task<SecondArticleDto> GetById(int id);
        Task Create(SecondArticleDto model);

        void Update(SecondArticleDto model);

        Task Delete(int id);




    }
}
