using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using System.Reflection;

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

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var champions = await _characterRepository.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(champions);

            return Ok(data);
        }

        [HttpGet("items")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetAllItems()
        {
            var items = await _itemRepository.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Item>, IReadOnlyList<ItemDto>>(items);

            return Ok(data);
        }

        [HttpGet("war")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetWarCharacters()
        {
            bool isShort = false;
            var champions = await _characterRepository.ListAllAsync();

            var warCharacters = WarChampions.GenerateWarChampions(champions);

            if (warCharacters.Count() == 0)
                return BadRequest();

            var mappedChampions = MapChampions(warCharacters);

            var result = WarChampions.SelectObjects(mappedChampions, isShort);

            return result == null ? BadRequest() : Ok(result);
        }

        [HttpGet("item-war")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetWarCharactersWithItems()
        {
            var isShort = true;

            var characterTask = await _characterRepository.ListAllAsync();
            var itemsTask = await _itemRepository.ListAllAsync();

            var warCharacters = WarChampions.GenerateWarChampions(characterTask);
            var items = WarChampions.GenerateItems(itemsTask);

            var (championsList, itemsList) = MapAndMergeDtos(warCharacters, items);

            var champions = WarChampions.SelectObjects(championsList, isShort);

            if (champions is null || champions.Count() == 0)
                return BadRequest();

            var result =  WarChampions.CreateChampionsWithItemList(champions, itemsList);

            return result == null ? BadRequest() : Ok(result);
        }

        #region Private Methods

        private (List<CharacterDto>, List<ItemDto>) MapAndMergeDtos(List<Character> warCharacters, List<Item> items)
        {
            var championsList = _mapper.Map<List<CharacterDto>>(warCharacters);
            var itemsList = _mapper.Map<List<ItemDto>>(items);

            WarChampions.MergeChampionWithItems(championsList[0], new List<ItemDto> { itemsList[0], itemsList[1] });
            WarChampions.MergeChampionWithItems(championsList[1], new List<ItemDto> { itemsList[2], itemsList[3] });

            return (championsList, itemsList);
        }
        private List<CharacterDto> MapChampions(List<Character> characters) => new List<CharacterDto>
            {
                _mapper.Map<Character, CharacterDto>(characters[0]), _mapper.Map<Character, CharacterDto>(characters[1])
            };
 
        #endregion  
    }
}
