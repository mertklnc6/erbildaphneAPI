
using erbildaphneAPI.Entity.DTOs;

namespace erbildaphneAPI.Entity.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllAsync();
        Task<ArticleDto> GetById(int id);
        Task Create(ArticleDto model);

        void Update(ArticleDto model);

        Task Delete(int id);




    }
}
