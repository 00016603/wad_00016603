using AutoMapper;
using NewsPaperAPI_00016603.DTOs;
using NewsPaperAPI_00016603.Models;

namespace NewsPaperAPI_00016603.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsDTO, News>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
