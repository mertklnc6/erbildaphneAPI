using AutoMapper;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
using erbildaphneAPI.Entity.Services;
using erbildaphneAPI.Entity.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erbildaphneAPI.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CommentService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CommentDto> GetById(int id)
        {
            try
            {
                var comment = await _uow.GetRepository<Comment>().GetById(id);
                if (comment == null)
                {
                    throw new KeyNotFoundException($"Kayıt bulunamadı: ID={id}");
                }
                return _mapper.Map<CommentDto>(comment);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("GetById işlemi sırasında hata oluştu", ex);
            }
        }



        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            try
            {
                var list = await _uow.GetRepository<Comment>().GetAll();
                return _mapper.Map<IEnumerable<CommentDto>>(list);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Listeyi getirme işlemi sırasında hata oluştu", ex);
            }
        }





        public async Task Create(CommentDto model)
        {
            //try
            //{
            var comment = _mapper.Map<Comment>(model);
            await _uow.GetRepository<Comment>().Create(comment);
            await _uow.CommitAsync();
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    throw new Exception("Create işlemi sırasında hata oluştu", ex);
            //}
        }


        public void Update(CommentDto model)
        {

            var comment = new Comment();
            comment.Id = model.Id;
            comment.Content = model.Content;
            comment.IsVerified = model.IsVerified;
            comment.IsDeleted = model.IsDeleted;
            comment.Content = model.Content;
            comment.Name = model.Name;
            comment.CreatedDate = model.CreatedDate;
            comment.Email = model.Email;           
            comment.Title = model.Title;
            _uow.GetRepository<Comment>().Update(comment);
            _uow.Commit();

        }


        public async Task Delete(int id)
        {
            try
            {
                var comment = await _uow.GetRepository<Comment>().GetById(id);
                if (comment == null)
                {
                    throw new KeyNotFoundException($"Silinmek için kayıt bulunamadı: ID={id}");
                }

                _uow.GetRepository<Comment>().Delete(comment);
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
