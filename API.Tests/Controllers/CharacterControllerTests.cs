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
        public CharacterControllerTests()
        {

        }
        [Fact]
        public async Task GetCharacters_Returns_OkResult()
        {
            // Arrange
            var mockService = new Mock<IYourService>();
            mockService.Setup(s => s.GetCharacters()).ReturnsAsync(new List<CharacterDto> { /* characters here */ });
            var controller = new YourController(mockService.Object);

            // Act
            var result = await controller.GetCharacters();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.IsType<List<CharacterDto>>(okResult.Value);
        }
    }
}
