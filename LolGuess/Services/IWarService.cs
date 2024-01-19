using API.DTO.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IWarService
    {
        Task<IReadOnlyList<CharacterDto>> GetCharacters();
        Task<IReadOnlyList<ItemDto>> GetAllItems();
        Task<IEnumerable<object>> GetWarCharacters();
        Task<ChampionItemDto> GetWarCharactersWithItems();
    }
}
