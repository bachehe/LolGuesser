using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Character, CharacterDto>()
                .ForMember(x => x.PictureUrl, o => o.MapFrom<CharacterUrlResolver>());
        }
    }
}
