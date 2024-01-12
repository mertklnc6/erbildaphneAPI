using erbildaphneAPI.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erbildaphneAPI.Entity.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();

        Task<CommentDto> GetById(int id);
        Task Create(CommentDto model);

        void Update(CommentDto model);

        Task Delete(int id);
    }
}
