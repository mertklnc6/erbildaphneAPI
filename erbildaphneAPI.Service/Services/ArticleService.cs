using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ArticleService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ArticleDto> GetById(int id)
        {
            try
            {
                var article = await _uow.GetRepository<Article>().GetById(id);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<ArticleDto>(article);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<ArticleDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<Article>().GetAll();
                return _mapper.Map<IEnumerable<ArticleDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(ArticleDto model)
        {
            //try
            //{
            var article = _mapper.Map<Article>(model);
            await _uow.GetRepository<Article>().Create(article);
            await _uow.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    throw new Exception("Create işlemi sırasında hata oluştu", ex);
            //}
        }


        public void Update(ArticleDto model)
        {

            var article = new Article();
            article.Id = model.Id;
            article.AuthorId = model.AuthorId;
            article.IsPublished = model.IsPublished;
            article.IsDeleted = model.IsDeleted;
            article.Content = model.Content;
            article.PictureUrl = model.PictureUrl;
            article.CreatedDate = model.CreatedDate;
            article.Description = model.Description;
            article.IsBoosted = model.IsBoosted;
            article.IsChosen = model.IsChosen;
            article.Title = model.Title;
            _uow.GetRepository<Article>().Update(article);
            _uow.Commit();

        }


        public async Task Delete(int id)
        {
            try
            {
                var article = await _uow.GetRepository<Article>().GetById(id);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<Article>().Delete(article);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Delete işlemi sırasında hata oluştu", ex);
            }
        }

    }
}
