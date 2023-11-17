using API.Controllers;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Controllers
{
    public class CharacterControllerTest
    {
        private readonly Mock<IGenericRepository<Character>> _mockRepo;
        private readonly Mock<IGenericRepository<Item>> _mockItem;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CharacterController _controller;

        public CharacterControllerTest()
        {
            _mockRepo = new Mock<IGenericRepository<Character>>();
            _mockItem = new Mock<IGenericRepository<Item>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CharacterController(_mockRepo.Object, _mockMapper.Object, _mockItem.Object);
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

            _mockRepo.Setup(repo => repo.ListAllAsync()).ReturnsAsync(characters);
            _mockMapper.Setup(mapper => mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(It.IsAny<IReadOnlyList<Character>>()))
                       .Returns(characterDtos);

            var result = await _controller.GetCharacters();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CharacterDto>>(actionResult.Value);
            Assert.Equal(characterDtos.Count, returnValue.Count);
            _mockRepo.Verify(repo => repo.ListAllAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IReadOnlyList<Character>, IReadOnlyList<CharacterDto>>(It.IsAny<IReadOnlyList<Character>>()), Times.Once);
        }

        [Fact]
        public async Task GetWar_CheckType()
        {
            var fakeCharacters = new List<Character>
            {
                new Character { },
                new Character { }
            };
            var fakeWarCharacters = new List<Character>(fakeCharacters);

            var fakeCharacterDtos = new List<CharacterDto>
            {
                new CharacterDto { },
                new CharacterDto {  }
            };

            _mockRepo.Setup(repo => repo.ListAllAsync()).ReturnsAsync(fakeCharacters);

            _mockMapper.Setup(m => m.Map<Character, CharacterDto>(It.IsAny<Character>()))
                .Returns<Character>(src => fakeCharacterDtos.FirstOrDefault(dto => dto.Name == src.Name));

            var result = await _controller.GetWarCharacters();
            Assert.Equal(typeof(ActionResult<IReadOnlyList<CharacterDto>>), result.GetType());
        }
        [Fact]
        public async Task GetWarCharactersWithItems_ReturnsBadRequest_NotMapped()
        {
            var ch1 = new Character()
            {
                Ad = 5,

            };
            var ch2 = new Character()
            {
                Ad = 52,
            };
            var i = new Item()
            {
                Ad = 5,
            };

            var characters = new List<Character> { ch1,ch2 };
            var items = new List<Item> {i,i,i,i};

            _mockRepo.Setup(repo => repo.ListAllAsync()).ReturnsAsync(characters);
            _mockItem.Setup(repo => repo.ListAllAsync()).ReturnsAsync(items);

            var result = await _controller.GetWarCharactersWithItems();

            var okResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.NotNull(okResult);

        }

        [Fact]
        public async Task GetAllItems_ReturnsOkResult_WithItems()
        {
            var items = new List<Item> { new Item() { Ad = 1 }, new Item() { Ad = 2 } };
            var itemDtos = new List<ItemDto> { new ItemDto() { Ad = 1 }, new ItemDto() { Ad = 1 } };

            _mockItem.Setup(repo => repo.ListAllAsync()).ReturnsAsync(items);
            _mockMapper.Setup(m => m.Map<IReadOnlyList<Item>, IReadOnlyList<ItemDto>>(It.IsAny<IReadOnlyList<Item>>()))
                       .Returns(itemDtos);

            var result = await _controller.GetAllItems();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ItemDto>>(actionResult.Value);
            Assert.Equal(itemDtos, returnValue);
        }
    }
}
