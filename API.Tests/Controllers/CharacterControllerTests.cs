using API.Controllers;
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
            var characters = new List<CharacterDto> { new CharacterDto {} };
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
    }
}
