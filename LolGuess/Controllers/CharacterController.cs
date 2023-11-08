using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace API.Controllers
{
    public class CharacterController : BaseController
    {
        private const string NameAttribute = "Name";
        private const string PicAttribute = "PictureUrl";
        private const string ManaAttribute = "Mana";
        private const int ManaChecker = 10;

        private readonly IGenericRepository<Character> _characterRepository;
        private readonly IMapper _mapper;

        public CharacterController(IGenericRepository<Character> characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var champions = await _characterRepository.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(champions);

            return Ok(data);
        }

        [HttpGet("war")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetWarCharacters()
        {
            bool isShort = false;
            var champions = await _characterRepository.ListAllAsync();

            var warCharacters = WarChampions.Generate(champions);

            var firstChampion = _mapper.Map<Character, CharacterDto>(warCharacters[0]);
            var secondChampion = _mapper.Map<Character, CharacterDto>(warCharacters[1]);

            var res = WarChampions.SelectObjects(firstChampion, secondChampion, false);

            if (res == null)
                return BadRequest();


            return Ok(res);
        }

        [HttpGet("random-item")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetItemChampions()
        {
            var isShort = true;
            var champions = await _characterRepository.ListAllAsync();

            var item1 = new ItemDto()
            {
                Ad = 500,
                As = 500,
                Armor = 500,
                Name = "Dupa",
                Hp = 500,
                Mana = 500,
                Mr = 500,
                MS = 500,
                PictureUrl = "picture",
                Range = 500,

            }; 

            var item2 = new ItemDto()
            {
                Ad = 500,
                As = 500,
                Armor = 500,
                Name = "Dupa",
                Hp = 500,
                Mana = 500,
                Mr = 500,
                MS = 500,
                PictureUrl = "picture",
                Range = 500,

            };

            var warCharacters = WarChampions.Generate(champions);

            var firstChampion = _mapper.Map<Character, CharacterDto>(warCharacters[0]);
            var secondChampion = _mapper.Map<Character, CharacterDto>(warCharacters[1]);

            MergeChampionWithItem(firstChampion, item1);
            MergeChampionWithItem(secondChampion, item2);

            var res = WarChampions.SelectObjects(firstChampion, secondChampion, isShort);

            var champsItems = new ChampionItemDto()
            {
                Character = res,
                Item = item1.Name,
                ItemPictureUrl = item1.PictureUrl,
            };


            return Ok(champsItems);
        }

        private void MergeChampionWithItem(CharacterDto champion, ItemDto item)
        {
            foreach (var property in typeof(CharacterDto).GetProperties())
            {
                var itemProperty = typeof(ItemDto).GetProperty(property.Name);

                if (itemProperty == null) continue;

                var championValue = property.GetValue(champion);
                var itemValue = itemProperty.GetValue(item);

                if (championValue == null || itemValue == null) continue;

                if (property.Name == NameAttribute || property.Name == PicAttribute) continue;

                if (property.Name == ManaAttribute && (decimal)championValue < ManaChecker) continue;

                if (property.PropertyType == typeof(string))    
                    property.SetValue(champion, (string)championValue + (string)itemValue);
                
                else if (property.PropertyType == typeof(decimal))          
                    property.SetValue(champion, (decimal)championValue + (decimal)itemValue);             
            }
        }
    }
}
