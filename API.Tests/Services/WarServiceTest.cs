using API.DTO.Character;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Services
{
    public class WarServiceTest
    {
        private readonly Mock<IGenericRepository<Character>> _mockCharacter;
        private readonly Mock<IGenericRepository<Item>> _mockItem;
        private readonly Mock<IMapper> _mockMapper;
        private readonly WarService _warService;
        public WarServiceTest()
        {
            _mockCharacter = new Mock<IGenericRepository<Character>>();
            _mockItem = new Mock<IGenericRepository<Item>>();
            _mockMapper = new Mock<IMapper>();
            _warService = new WarService(_mockCharacter.Object, _mockMapper.Object, _mockItem.Object);
        }
        [Fact]
        public async Task GetCharacters_ReturnsOkObjectResult_WithListOfCharacterDtos()
        {
            var characters = new List<Character>
            {
                new Character {},
                new Character {}
            };

            var characterDtos = new List<CharacterDto>
            {
                new CharacterDto {},
                new CharacterDto {}
            };

            _mockCharacter.Setup(repo => repo.ListAllAsync()).ReturnsAsync(characters);

            _mockMapper.Setup(mapper =>
                mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(
                    It.Is<IReadOnlyList<Character>>(list => list.Count == characters.Count)))
                .Returns(characterDtos);

            var result = await _warService.GetCharacters();

            var returnValue = Assert.IsType<List<CharacterDto>>(result);

            Assert.Equal(characterDtos.Count, returnValue.Count);

            _mockCharacter.Verify(repo => repo.ListAllAsync(), Times.Once);

            _mockMapper.Verify(mapper =>
                mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(
                    It.Is<IReadOnlyList<Character>>(list => list.Count == characters.Count)),
                Times.Once);
        }

        [Fact]
        public async Task GetWarCharacters_ReturnsListOfCharacterDto()
        {
            var fakeCharacters = new List<Character>
            {
                new Character { Name = "Champion1", /* Set other properties */ },
                new Character { Name = "Champion2", /* Set other properties */ }
            };

            var fakeMappedCharacters = new List<CharacterDto>
            {
                new CharacterDto { Name = "Champion1", /* Set other properties */ },
                new CharacterDto { Name = "Champion2", /* Set other properties */ }
            };

            _mockCharacter.Setup(repo => repo.ListAllAsync()).ReturnsAsync(fakeCharacters);

            _mockMapper.Setup(m => m.Map<CharacterDto>(It.IsAny<Character>()))
                .Returns<Character>(src => fakeMappedCharacters.FirstOrDefault(dto => dto.Name == src.Name));

            var resultTask = _warService.GetWarCharacters();
            var result = await resultTask;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllItems_ReturnsListOfItemDto()
        {

            var items = new List<Item> { new Item() { Ad = 1 }, new Item() { Ad = 2 } };
            var itemDtos = new List<ItemDto> { new ItemDto() { Ad = 1 }, new ItemDto() { Ad = 1 } };

            _mockItem.Setup(repo => repo.ListAllAsync()).ReturnsAsync(items);
            _mockMapper.Setup(m => m.Map<IReadOnlyList<Item>, IReadOnlyList<ItemDto>>(It.IsAny<IReadOnlyList<Item>>()))
                       .Returns(itemDtos);

            var resultTask = _warService.GetAllItems();
            var result = await resultTask;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReadOnlyList<ItemDto>>(result);
        }

        [Fact]
        public async Task GetWarCharactersWithItems_ReturnsListOfItemDto()
        {
            var warCharacters = new List<Character>
            {
                new Character { Name = "Champion1", PictureUrl ="url"},
                new Character { Name = "Champion2", PictureUrl ="url" }
            };

            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Item1", PictureUrl ="url" },
                new Item { Id = 2, Name = "Item2", PictureUrl ="url" },
                new Item { Id = 3, Name = "Item3", PictureUrl ="url" },
                new Item { Id = 4, Name = "Item4", PictureUrl ="url" }
            };

            var expectedChampionsList = new List<CharacterDto>
            {
                new CharacterDto { Name = "Champion1", PictureUrl ="url" },
                new CharacterDto { Name = "Champion2", PictureUrl ="url" }
            };

            var expectedItemsList = new List<ItemDto>
            {
                new ItemDto { Name = "Item1", PictureUrl ="url" },
                new ItemDto { Name = "Item2", PictureUrl ="url" },
                new ItemDto { Name = "Item3", PictureUrl ="url" },
                new ItemDto { Name = "Item4", PictureUrl ="url" }
            };

            _mockCharacter.Setup(repo => repo.ListAllAsync()).ReturnsAsync(warCharacters);
            _mockItem.Setup(repo => repo.ListAllAsync()).ReturnsAsync(items);

            _mockMapper.Setup(m => m.Map<List<ItemDto>>(It.IsAny<List<Item>>())).Returns(expectedItemsList);

            _mockMapper.Setup(m => m.Map<List<CharacterDto>>(It.IsAny<List<Character>>()))
                 .Returns(expectedChampionsList);

            var resultTask = _warService.GetWarCharactersWithItems();
            var result = await resultTask;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ChampionItemDto>(result);
        }
    }
}
