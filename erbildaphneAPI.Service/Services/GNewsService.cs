using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class GNewsService : IGNewsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GNewsService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GNewsDto> GetById(int id)
        {
            try
            {
                var gNews = await _uow.GetRepository<GNews>().GetById(id);
                if (gNews == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<GNewsDto>(gNews);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<GNewsDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<GNews>().GetAll();
                return _mapper.Map<IEnumerable<GNewsDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(GNewsDto model)
        {
            try
            {
                var gNews = _mapper.Map<GNews>(model);


                // Veritabanına kaydet
                await _uow.GetRepository<GNews>().Create(gNews);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("CreateGNewsWithSources işlemi sırasında hata oluştu", ex);
            }
        }


        public void Update(GNewsDto model)
        {
            try
            {

                _uow.GetRepository<GNews>().Update(_mapper.Map<GNews>(model));
                _uow.Commit();
            }
            catch (Exception ex)
            {
                // Hata yakalama ve loglama
                throw new Exception("Update işlemi sırasında hata oluştu", ex);
            }
        }


        public async Task Delete(int id)
        {
            try
            {
                var gNews = await _uow.GetRepository<GNews>().GetById(id);
                if (gNews == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<GNews>().Delete(gNews);
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
