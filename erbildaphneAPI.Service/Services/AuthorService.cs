using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AuthorService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<AuthorDto> GetById(int id)
        {
            try
            {
                var author = await _uow.GetRepository<Author>().GetById(id);
                if (author == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<AuthorDto>(author);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<Author>().GetAll();
                return _mapper.Map<IEnumerable<AuthorDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }


        public async Task Create(AuthorDto model)
        {
            try
            {
                var author = _mapper.Map<Author>(model);
                await _uow.GetRepository<Author>().Create(author);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Create işlemi sırasında hata oluştu", ex);
            }
        }


        public void Update(AuthorDto model)
        {
            try
            {
                _uow.GetRepository<Author>().Update(_mapper.Map<Author>(model));
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
                var author = await _uow.GetRepository<Author>().GetById(id);
                if (author == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<Author>().Delete(author);
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
