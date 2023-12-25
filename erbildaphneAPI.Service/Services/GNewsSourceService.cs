using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class GNewsSourceService : IGNewsSourceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GNewsSourceService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<GNewsSourceDto> GetById(int id)
        {
            try
            {
                var gNewsSource = await _uow.GetRepository<GNewsSource>().GetById(id);
                if (gNewsSource == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<GNewsSourceDto>(gNewsSource);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<GNewsSourceDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<GNewsSource>().GetAll();
                return _mapper.Map<IEnumerable<GNewsSourceDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(GNewsSourceDto model)
        {
            try
            {
                var gNewsSource = _mapper.Map<GNewsSource>(model);
                await _uow.GetRepository<GNewsSource>().Create(gNewsSource);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Create işlemi sırasında hata oluştu", ex);
            }
        }


        public void Update(GNewsSourceDto model)
        {
            try
            {


                // Veritabanında güncelleme
                _uow.GetRepository<GNewsSource>().Update(_mapper.Map<GNewsSource>(model));
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
                var gNewsSource = await _uow.GetRepository<GNewsSource>().GetById(id);
                if (gNewsSource == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<GNewsSource>().Delete(gNewsSource);
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
