using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CharacterController : BaseController
    {
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
            var champions = await _characterRepository.ListAllAsync();

            var warCharacters = WarChampions.Generate(champions);

            var firstChampion = _mapper.Map<Character, CharacterDto>(warCharacters[0]);
            var secondCharacter = _mapper.Map<Character, CharacterDto>(warCharacters[1]);

            var result = MappedList(firstChampion, secondCharacter);

            return result;
        }
        private ActionResult<IReadOnlyList<CharacterDto>> MappedList(CharacterDto ch1, CharacterDto ch2)
        {
            if (ch1 == null || ch2 == null)
                return BadRequest();

            var war = new List<CharacterDto>() { ch1, ch2 };

            var randomIndex = EnumHelper.GetEnumValues();

            var selector = WarChampions.GetSelector((PropertyEnum)randomIndex);

            return selector == null ? BadRequest() : Ok(war.Select(selector));
        } 
    }
}
