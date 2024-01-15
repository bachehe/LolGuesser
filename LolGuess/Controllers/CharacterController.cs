using API.DTO;
using API.Helpers;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CharacterController : BaseController
    {

        private readonly IWarService _service;

        public CharacterController(IGenericRepository<Character> characterRepository, IMapper mapper, IGenericRepository<Item> itemRepository, IWarService service)
        {

            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var champions = await _service.GetCharacters();
            return champions == null ? BadRequest() : Ok(champions);
        }

        [HttpGet("items")]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAllItems()
        {
            var items = await _service.GetAllItems();
            return items == null ? BadRequest() : Ok(items);
        }

        [HttpGet("war")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetWarCharacters()
        {
            var warCharacters = await _service.GetWarCharacters();
            return warCharacters == null ? BadRequest() : Ok(warCharacters);
        }

        [HttpGet("item-war")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetWarCharactersWithItems()
        {
            var warCharacters = await _service.GetWarCharactersWithItems();
            return warCharacters == null ? BadRequest() : Ok(warCharacters);
        }
    }
}
