using API.DTO.Character;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{                                                                               //out string
    public class CharacterUrlResolver : IValueResolver<Character, CharacterDto, string>
    {
        private readonly IConfiguration _cfg;

        public CharacterUrlResolver(IConfiguration cfg)
        {
            _cfg = cfg;
        }
        public string Resolve(Character source, CharacterDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            return _cfg["ApiUrl"] + source.PictureUrl;
        }
  
    }
}
