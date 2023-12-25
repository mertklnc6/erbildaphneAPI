using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class RumorService : IRumorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RumorService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<RumorDto> GetById(int id)
        {
            try
            {
                var rumor = await _uow.GetRepository<Rumor>().GetById(id);
                if (rumor == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<RumorDto>(rumor);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<RumorDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<Rumor>().GetAll();
                return _mapper.Map<IEnumerable<RumorDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(RumorDto model)
        {
            try
            {
                var rumor = _mapper.Map<Rumor>(model);
                await _uow.GetRepository<Rumor>().Create(rumor);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Create işlemi sırasında hata oluştu", ex);
            }
        }


        public void Update(RumorDto model)
        {
            try
            {

                _uow.GetRepository<Rumor>().Update(_mapper.Map<Rumor>(model));
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
                var rumor = await _uow.GetRepository<Rumor>().GetById(id);
                if (rumor == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<Rumor>().Delete(rumor);
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
