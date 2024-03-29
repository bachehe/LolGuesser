﻿using API.DTO.Character;
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

            CreateMap<Item, ItemDto>()
                 .ForMember(x => x.PictureUrl, o => o.MapFrom<ItemUrlResolver>());
        }
    }
}
