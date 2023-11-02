using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace API.Tests.Helpers
{
    public class CharacterUrlResolverTest
    {
        [Fact]
        public void Resolve_ReturnsEmptyString_EmptyUrl()
        {
            var cfgMock = new Mock<IConfiguration>();
            var resolver = new CharacterUrlResolver(cfgMock.Object);
            var source = new Character { PictureUrl = null };
            var destination = new CharacterDto();

            var result = resolver.Resolve(source, destination, nameof(destination.PictureUrl), null);

            Assert.Equal(string.Empty, result);
        }
        [Theory]
        [InlineData("images/Champions/Sona.jpg")]
        [InlineData("images/Champions/Aatrox.jpg")]
        [InlineData("images/Champions/Akali.jpg")]
        [InlineData("images/Champions/Swain.jpg")]
        [InlineData("images/Champions/Kennen.jpg")]
        public void Resolve_ReturnsFullUrl_WhenPictureUrlIsNotEmpty(string url)
        {
            var expectedApiUrl = "https://localhost:7224/";
            var cfgMock = new Mock<IConfiguration>();
            cfgMock.Setup(c => c["ApiUrl"]).Returns(expectedApiUrl);
            var resolver = new CharacterUrlResolver(cfgMock.Object);
            var source = new Character { PictureUrl = url };
            var destination = new CharacterDto();

            var result = resolver.Resolve(source, destination, nameof(destination.PictureUrl), null);

            var expectedUrl = expectedApiUrl + url;
            Assert.Equal(expectedUrl, result);
        }
    }
}
