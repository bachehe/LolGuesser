using API.DTO;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CharacterController : BaseController
    {

        private readonly IGenericRepository<Character> _characterRepository;

        public CharacterController(IGenericRepository<Character> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var champions = await _characterRepository.ListAllAsync();

            return Ok(champions);
        }
        [HttpGet("war")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetWarCharacters()
        {
            var champions = await _characterRepository.ListAllAsync();

            var warCharacters = WarChampions.Generate(champions);
            var randomIndex = new Random().Next(2, 6);

            return (PropertyEnum)randomIndex switch
            {
                PropertyEnum.Hp => Ok(warCharacters.Select(x => new { x.Name, x.Hp })),
                PropertyEnum.Ad => Ok(warCharacters.Select(x => new { x.Name, x.Ad })),
                PropertyEnum.Ap => Ok(warCharacters.Select(x => new { x.Name, x.Ap })),
                PropertyEnum.HpGain => Ok(warCharacters.Select(x => new { x.Name, x.HpGain })),
                _ => BadRequest()
            };
        }
    }
}
