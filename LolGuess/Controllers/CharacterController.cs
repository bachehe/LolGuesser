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
            //var data = _mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(warCharacters);

            //var temp = data.Select(x => x.PictureUrl);

            var randomIndex = new Random().Next(3, 13);

            return (PropertyEnum)randomIndex switch
            {
                PropertyEnum.Hp => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Hp })),
                PropertyEnum.HpGain => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.HpGain })),
                PropertyEnum.Mana => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Mana })),
                PropertyEnum.ManaGain => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.ManaGain })),
                PropertyEnum.Ad => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Ad })),
                PropertyEnum.As => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.As })),
                PropertyEnum.Armor => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Armor })),
                PropertyEnum.ArmorGain => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.ArmorGain })),
                PropertyEnum.Mr => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Mr })),
                PropertyEnum.MS => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.MS })),
                PropertyEnum.Range => Ok(warCharacters.Select(x => new { x.Name, x.PictureUrl, x.Range })),
                _ => BadRequest()
            };
        }
    }
}
