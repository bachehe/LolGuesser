using API.DTO;
using API.Helpers;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CharacterController : BaseController
    {

        private readonly IWarService _service;

        public CharacterController(IWarService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var champions = await _service.GetCharacters();

            if (champions == null) return BadRequest();
            
            return champions.Any() ? Ok(champions) : NoContent();
        }

        [HttpGet("items")]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAllItems()
        {
            var items = await _service.GetAllItems();

            if (items == null) return BadRequest();       

            return items.Any() ? Ok(items) : NoContent();
        }

        [HttpGet("war")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetWarCharacters()
        {
            var warCharacters = await _service.GetWarCharacters();

            if (warCharacters == null) return BadRequest();

            return warCharacters.Any() ? Ok(warCharacters) : NoContent();
        }

        [HttpGet("item-war")]
        public async Task<ActionResult<IReadOnlyList<ChampionItemDto>>> GetWarCharactersWithItems()
        {
            var warCharacters = await _service.GetWarCharactersWithItems();

            return warCharacters == null ? BadRequest() : Ok(warCharacters);
        }
    }
}
