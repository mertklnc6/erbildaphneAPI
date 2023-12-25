using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.Service.Services
{
    public class WriteService : IWriteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WriteService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<WriteDto> GetById(int id)
        {
            try
            {
                var write = await _uow.GetRepository<Write>().GetById(id);
                if (write == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<WriteDto>(write);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<WriteDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<Write>().GetAll();
                return _mapper.Map<IEnumerable<WriteDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(WriteDto model)
        {
            //try
            //{
            var write = _mapper.Map<Write>(model);
            await _uow.GetRepository<Write>().Create(write);
            await _uow.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    throw new Exception("Create işlemi sırasında hata oluştu", ex);
            //}
        }


        public void Update(WriteDto model)
        {

            var write = new Write();
            write.Id = model.Id;
            write.AuthorId = model.AuthorId;
            write.IsPublished = model.IsPublished;
            write.IsDeleted = model.IsDeleted;
            write.Content = model.Content;
            write.PictureUrl = model.PictureUrl;
            write.CreatedDate = model.CreatedDate;
            write.Description = model.Description;
            write.IsBoosted = model.IsBoosted;
            write.IsChosen = model.IsChosen;
            write.Title = model.Title;
            _uow.GetRepository<Write>().Update(write);
            _uow.Commit();

        }


        public async Task Delete(int id)
        {
            try
            {
                var write = await _uow.GetRepository<Write>().GetById(id);
                if (write == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<Write>().Delete(write);
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
