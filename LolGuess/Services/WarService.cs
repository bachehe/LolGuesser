using API.DTO.Character;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace API.Services
{
    public interface IWarService
    {
        Task<IReadOnlyList<CharacterDto>> GetCharacters();
        Task<IReadOnlyList<ItemDto>> GetAllItems();
        Task<IEnumerable<object>> GetWarCharacters();
        Task<ChampionItemDto> GetWarCharactersWithItems();
    }
    public class WarService : IWarService
    {
        private readonly IGenericRepository<Character> _characterRepository;
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public WarService(IGenericRepository<Character> characterRepository, IMapper mapper, IGenericRepository<Item> itemRepository)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<IReadOnlyList<CharacterDto>> GetCharacters()
        {
            var champions = await _characterRepository.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(champions);

            return data;
        }
        public async Task<IReadOnlyList<ItemDto>> GetAllItems()
        {
            var items = await _itemRepository.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Item>, IReadOnlyList<ItemDto>>(items);

            return data;
        }
        public async Task<IEnumerable<object>> GetWarCharacters()
        {
            bool isShort = false;
            var champions = await _characterRepository.ListAllAsync();

            var warCharacters = WarChampions.GenerateWarChampions(champions);

            var mappedChampions = MapChampions(warCharacters);

            var result = WarChampions.SelectObjects(mappedChampions, isShort);

            return result;
        }
        public async Task<ChampionItemDto> GetWarCharactersWithItems()
        {
            var isShort = true;

            var characterTask = await _characterRepository.ListAllAsync();
            var itemsTask = await _itemRepository.ListAllAsync();

            var warCharacters = WarChampions.GenerateWarChampions(characterTask);
            var items = WarChampions.GenerateItems(itemsTask);

            var (championsList, itemsList) = MapAndMergeDtos(warCharacters, items);

            var champions = WarChampions.SelectObjects(championsList, isShort);

            var result = WarChampions.CreateChampionsWithItemList(champions, itemsList);

            return result;
        }
        private (List<CharacterDto>, List<ItemDto>) MapAndMergeDtos(List<Character> warCharacters, List<Item> items)
        {
            var championsList = _mapper.Map<List<CharacterDto>>(warCharacters);
            var itemsList = _mapper.Map<List<ItemDto>>(items);

            if (championsList == null || itemsList == null)
                return (championsList, itemsList);

            WarChampions.MergeChampionWithItems(championsList[0], new List<ItemDto> { itemsList[0], itemsList[1] });
            WarChampions.MergeChampionWithItems(championsList[1], new List<ItemDto> { itemsList[2], itemsList[3] });

            return (championsList, itemsList);
        }
        private List<CharacterDto> MapChampions(List<Character> characters) => new List<CharacterDto>
            {
                _mapper.Map<Character, CharacterDto>(characters[0]), _mapper.Map<Character, CharacterDto>(characters[1])
            };

    }
}
