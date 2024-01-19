using API.DTO.Character;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{                                                                               //out string
    public class ItemUrlResolver : IValueResolver<Item, ItemDto, string>
    {
        private readonly IConfiguration _cfg;

        public ItemUrlResolver(IConfiguration cfg)
        {
            _cfg = cfg;
        }
        public string Resolve(Item source, ItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            return _cfg["ApiUrl"] + source.PictureUrl;
        }
    }
}
