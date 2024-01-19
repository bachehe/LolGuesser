using API.Controllers;
using API.DTO.Character;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Controllers
{
    public class CharacterControllerTests
    {
        private readonly Mock<IWarService> _warService;
        public CharacterControllerTests()
        {
            _warService = new Mock<IWarService>();
        }
        [Fact]
        public async Task GetCharacters_Returns_OkResult_WithCharacters()
        {
            var characters = new List<CharacterDto> { new CharacterDto { } };
            _warService.Setup(s => s.GetCharacters()).ReturnsAsync(characters);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetCharacters();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCharacters = Assert.IsType<List<CharacterDto>>(okResult.Value);
            Assert.Equal(characters, returnedCharacters);
        }

        [Fact]
        public async Task GetCharacters_Returns_NoContent_When_ServiceReturnsEmptyList()
        {
            var characters = new List<CharacterDto>();
            _warService.Setup(s => s.GetCharacters()).ReturnsAsync(characters);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetCharacters();

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetCharacters_Returns_BadRequest_When_ServiceReturnsNull()
        {
            _warService.Setup(s => s.GetCharacters()).ReturnsAsync((List<CharacterDto>)null);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetCharacters();

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetItems_Returns_OkResult_WithCharacters()
        {
            var items = new List<ItemDto> { new ItemDto { } };
            _warService.Setup(s => s.GetAllItems()).ReturnsAsync(items);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetAllItems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var expected = Assert.IsType<List<ItemDto>>(okResult.Value);
            Assert.Equal(items, expected);
        }

        [Fact]
        public async Task GetItems_Returns_NoContent_When_ServiceReturnsEmptyList()
        {
            var items = new List<ItemDto>();
            _warService.Setup(s => s.GetAllItems()).ReturnsAsync(items);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetAllItems();

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetItems_Returns_BadRequest_When_ServiceReturnsNull()
        {
            _warService.Setup(s => s.GetAllItems()).ReturnsAsync((List<ItemDto>)null);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetAllItems();

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetWarCharacters_Returns_OkResult_WithCharacters()
        {
            var characters = new List<CharacterDto> { new CharacterDto { } };
            _warService.Setup(s => s.GetWarCharacters()).ReturnsAsync(characters);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetWarCharacters();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCharacters = Assert.IsType<List<CharacterDto>>(okResult.Value);
            Assert.Equal(characters, returnedCharacters);
        }

        [Fact]
        public async Task GetWarCharacters_Returns_NoContent_When_ServiceReturnsEmptyList()
        {
            var characters = new List<CharacterDto>();
            _warService.Setup(s => s.GetWarCharacters()).ReturnsAsync(characters);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetWarCharacters();

            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task GetWarCharacters_Returns_BadRequest_When_ServiceReturnsNull()
        {
            _warService.Setup(s => s.GetWarCharacters()).ReturnsAsync((List<CharacterDto>)null);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetWarCharacters();

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetWarCharactersWithItems_Returns_OkResult_WithCharacters()
        {
            object obj = new();
            var character = new ChampionItemDto()
            {
                Character = new List<object>() { obj, obj },
                Item = new List<string>() { "s", "s" },
                ItemPictureUrl = new List<string>() { "v", "v" }
            };

            _warService.Setup(s => s.GetWarCharactersWithItems()).ReturnsAsync(character);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetWarCharactersWithItems();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCharacters = Assert.IsType<ChampionItemDto>(okResult.Value);
            Assert.Equal(character, returnedCharacters);
        }


        [Fact]
        public async Task GetWarCharactersWithItems_Returns_NoContent_When_ServiceReturnsEmptyList()
        {
            var character = new ChampionItemDto()
            {
                Character = new List<object>(),
                Item = new List<string>(),
                ItemPictureUrl = new List<string>()
            };

            _warService.Setup(s => s.GetWarCharactersWithItems()).ReturnsAsync(character);
            var controller = new CharacterController(_warService.Object);

            var result = await controller.GetWarCharactersWithItems();

            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
