using AutoMapper;
using erbildaphneAPI.DataAccess.Identity.Models;
using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Entities;
namespace erbildaphneAPI.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Rumor, RumorDto>().ReverseMap();
            CreateMap<GNews, GNewsDto>().ReverseMap();
            CreateMap<GNewsSource, GNewsSourceDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppRole, RoleDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<SecondArticle, SecondArticleDto>().ReverseMap();            
            CreateMap<MainNews, MainNewsDto>().ReverseMap();


        }
    }
}
