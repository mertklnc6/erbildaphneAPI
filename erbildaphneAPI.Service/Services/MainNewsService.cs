using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class MainNewsService : IMainNewsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MainNewsService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<MainNewsDto> GetById(int id)
        {
            try
            {
                var mNews = await _uow.GetRepository<MainNews>().GetById(id);
                if (mNews == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<MainNewsDto>(mNews);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<MainNewsDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<MainNews>().GetAll();
                return _mapper.Map<IEnumerable<MainNewsDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(MainNewsDto model)
        {
            //try
            //{
            var mNews = _mapper.Map<MainNews>(model);
            await _uow.GetRepository<MainNews>().Create(mNews);
            await _uow.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    throw new Exception("Create işlemi sırasında hata oluştu", ex);
            //}
        }


        public void Update(MainNewsDto model)
        {

            var mNews = new MainNews();
            mNews.Id = model.Id;            
            mNews.IsPublished = model.IsPublished;
            mNews.IsDeleted = model.IsDeleted;
            mNews.Content = model.Content;
            mNews.PictureUrl = model.PictureUrl;
            mNews.CreatedDate = model.CreatedDate;
            mNews.Description = model.Description;
            mNews.IsBoosted = model.IsBoosted;
            mNews.IsChosen = model.IsChosen;
            mNews.Title = model.Title;
            _uow.GetRepository<MainNews>().Update(mNews);
            _uow.Commit();

        }


        public async Task Delete(int id)
        {
            try
            {
                var mNews = await _uow.GetRepository<MainNews>().GetById(id);
                if (mNews == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<MainNews>().Delete(mNews);
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
