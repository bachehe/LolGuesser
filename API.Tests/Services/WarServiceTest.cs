using API.DTO;
using API.Services;
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
    }
}
