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
        private readonly IGenericRepository<Character> _characterRepository;
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public CharacterController(IGenericRepository<Character> characterRepository, IMapper mapper, IGenericRepository<Item> itemRepository)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
            _itemRepository = itemRepository;
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

            var res = WarChampions.SelectObjects(firstChampion, secondChampion, isShort);

            if (res == null)
                return BadRequest();


            return Ok(res);
        }

        [HttpGet("random-item")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetItemChampions()
        {
            var isShort = true;
            var champions = await _characterRepository.ListAllAsync();

           //create and seed
           // var items = await _itemRepository.ListAllAsync();

            var item1 = new ItemDto()
            {
                Ad = 500,
                As = 0.5m,
                Armor = 500,
                Name = "Dupa",
                Hp = 500,
                Mana = 500,
                Mr = 500,
                MS = 0.5m,
                PictureUrl = "picture",

            }; 

            var item2 = new ItemDto()
            {
                Ad = 500,
                As = 0.5m,
                Armor = 500,
                Name = "Dupa",
                Hp = 500,
                Mana = 500,
                Mr = 500,
                MS = 0.5m,
                PictureUrl = "picture",

            };

            var warCharacters = WarChampions.Generate(champions);

            var firstChampion = _mapper.Map<Character, CharacterDto>(warCharacters[0]);
            var secondChampion = _mapper.Map<Character, CharacterDto>(warCharacters[1]);

            WarChampions.MergeChampionWithItem(firstChampion, item1);
            WarChampions.MergeChampionWithItem(secondChampion, item2);

            var championsList = WarChampions.SelectObjects(firstChampion, secondChampion, isShort);

            var champsItems = new ChampionItemDto()
            {
                Character = championsList,
                Item = new List<string> { item1.Name, item2.Name },
                ItemPictureUrl = new List<string> { item1.PictureUrl, item2.PictureUrl },
            };


            return Ok(champsItems);
        }
    }
}
