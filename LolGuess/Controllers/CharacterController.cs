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

        //[HttpGet("random-item")]
        //public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetItemChampions()
        //{
        //    var isShort = true;
        //    var champions = await _characterRepository.ListAllAsync();
        //    var itemsRepository = await _itemRepository.ListAllAsync();

        //    var warCharacters = WarChampions.Generate(champions);
        //    var items = WarChampions.GetItems(itemsRepository);

        //    var firstChampion = _mapper.Map<Character, CharacterDto>(warCharacters[0]);
        //    var secondChampion = _mapper.Map<Character, CharacterDto>(warCharacters[1]);

        //    var firstItem = _mapper.Map<Item, ItemDto>(items[0]);
        //    var secondItem = _mapper.Map<Item, ItemDto>(items[1]);


        //    WarChampions.MergeChampionWithItem(firstChampion,firstItem);
        //    WarChampions.MergeChampionWithItem(secondChampion, secondItem);

        //    var championsList = WarChampions.SelectObjects(firstChampion, secondChampion, isShort);

        //    var champsItems = new ChampionItemDto()
        //    {
        //        Character = championsList,
        //        Item = new List<string> { firstItem.Name, secondItem.Name },
        //        ItemPictureUrl = new List<string> { firstItem.PictureUrl, secondItem.PictureUrl },
        //    };

        //    return Ok(champsItems);
        //}
        [HttpGet("random-item")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetItemChampions()
        {
            var isShort = true;

            var characterTask = _characterRepository.ListAllAsync();
            var itemsTask = _itemRepository.ListAllAsync();
            await Task.WhenAll(characterTask, itemsTask);

            var champions = characterTask.Result;
            var itemsRepository = itemsTask.Result;

            var warCharacters = WarChampions.Generate(champions);
            var items = WarChampions.GetItems(itemsRepository);

            var firstChampion = MapCharacterToDto(warCharacters[0]);
            var secondChampion = MapCharacterToDto(warCharacters[1]);
            var firstItem = MapItemToDto(items[0]);
            var secondItem = MapItemToDto(items[1]);

            WarChampions.MergeChampionWithItem(firstChampion, firstItem);
            WarChampions.MergeChampionWithItem(secondChampion, secondItem);

            var championsList = WarChampions.SelectObjects(firstChampion, secondChampion, isShort);

            var champsItems = new ChampionItemDto
            {
                Character = championsList,
                Item = new List<string> { firstItem.Name, secondItem.Name },
                ItemPictureUrl = new List<string> { firstItem.PictureUrl, secondItem.PictureUrl },
            };

            return Ok(champsItems);
        }

        private CharacterDto MapCharacterToDto(Character character)
        {
            return _mapper.Map<Character, CharacterDto>(character);
        }

        private ItemDto MapItemToDto(Item item)
        {
            return _mapper.Map<Item, ItemDto>(item);
        }
    }
}
