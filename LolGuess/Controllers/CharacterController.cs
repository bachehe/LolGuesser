﻿using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Migrations;
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

        [HttpGet("champions")]
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

            var warCharacters = WarChampions.Generate(champions);

            if (warCharacters.Count() == 0)
                return BadRequest();

            var mappedChampions = MapChampions(warCharacters);

            var res = WarChampions.SelectObjects(mappedChampions[0], mappedChampions[1], isShort);

            if (res == null)
                return BadRequest();


            return Ok(res);
        }

        [HttpGet("item-war")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetItemChampions()
        {
            var isShort = true;

            var characterTask = _characterRepository.ListAllAsync();
            var itemsTask = _itemRepository.ListAllAsync();
            await Task.WhenAll(characterTask, itemsTask);

            var warCharacters = WarChampions.Generate(characterTask.Result);
            var items = WarChampions.GetItems(itemsTask.Result);

            var (championsList, itemsList) = CreateLists(warCharacters, items);

            WarChampions.MergeChampionWithItem(championsList[0], itemsList[0]);
            WarChampions.MergeChampionWithItem(championsList[1], itemsList[1]);

            var champions = WarChampions.SelectObjects(championsList[0], championsList[1], isShort);

            if (champions is null || champions.Count() == 0)
                return BadRequest();

            return Ok(new ChampionItemDto
            {
                Character = champions,
                Item = new List<string> { itemsList[0].Name, itemsList[1].Name },
                ItemPictureUrl = new List<string> { itemsList[0].PictureUrl, itemsList[1].PictureUrl },
            });
        }

        #region Private Methods
        private (List<CharacterDto>, List<ItemDto>) CreateLists(List<Character> warCharacters, List<Item> items)
        {
            var championsList = new List<CharacterDto>();
            var itemsList = new List<ItemDto>();

            for (int i = 0; i < 2; i++)
            {
                championsList.Add(_mapper.Map<Character, CharacterDto>(warCharacters[i]));
            }
            for (int i = 0; i < 2; i++)
            {
                itemsList.Add(_mapper.Map<Item, ItemDto>(items[i]));
            }

            return (championsList, itemsList);
        }
        private List<CharacterDto> MapChampions(List<Character> characters)
        {
            return new List<CharacterDto>
            {
                _mapper.Map<Character, CharacterDto>(characters[0]), _mapper.Map<Character, CharacterDto>(characters[1])
            };
        }
        #endregion  
    }
}
