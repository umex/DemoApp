using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<AppUser, UserDto>().ReverseMap().IncludeAllDerived();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}