using API.DTO;
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
            var pictureUrl = string.Empty;
            //TODO: source.PictureUrl
            if (string.IsNullOrEmpty(pictureUrl))
                return string.Empty;

            return _cfg["ApiUrl"] + pictureUrl;
        }
    }
}
