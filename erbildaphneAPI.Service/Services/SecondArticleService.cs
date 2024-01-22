using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class SecondArticleService : ISecondArticleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SecondArticleService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<SecondArticleDto> GetById(int id)
        {
            try
            {
                var sArticle = await _uow.GetRepository<SecondArticle>().GetById(id);
                if (sArticle == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<SecondArticleDto>(sArticle);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<SecondArticleDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<SecondArticle>().GetAll();
                return _mapper.Map<IEnumerable<SecondArticleDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(SecondArticleDto model)
        {
            //try
            //{
            var sArticle = _mapper.Map<SecondArticle>(model);
            await _uow.GetRepository<SecondArticle>().Create(sArticle);
            await _uow.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    throw new Exception("Create işlemi sırasında hata oluştu", ex);
            //}
        }


        public void Update(SecondArticleDto model)
        {

            var sArticle = new SecondArticle();
            sArticle.Id = model.Id;
            sArticle.AuthorId = model.AuthorId;
            sArticle.IsPublished = model.IsPublished;
            sArticle.IsDeleted = model.IsDeleted;
            sArticle.Content = model.Content;
            sArticle.PictureUrl = model.PictureUrl;
            sArticle.CreatedDate = model.CreatedDate;
            sArticle.Description = model.Description;
            sArticle.IsBoosted = model.IsBoosted;
            sArticle.IsChosen = model.IsChosen;
            sArticle.Title = model.Title;
            _uow.GetRepository<SecondArticle>().Update(sArticle);
            _uow.Commit();

        }


        public async Task Delete(int id)
        {
            try
            {
                var sArticle = await _uow.GetRepository<SecondArticle>().GetById(id);
                if (sArticle == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<SecondArticle>().Delete(sArticle);
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
