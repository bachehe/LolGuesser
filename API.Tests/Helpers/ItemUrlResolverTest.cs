using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel.DataAnnotations;


namespace API.Tests.Helpers
{
    public class ItemUrlResolverTest
    {
        [Fact]
        public void Resolve_ReturnsEmptyString_EmptyUrl()
        {
            var cfgMock = new Mock<IConfiguration>();
            var resolver = new ItemUrlResolver(cfgMock.Object);
            var source = new Item { PictureUrl = null };
            var destination = new ItemDto();

            var result = resolver.Resolve(source, destination, nameof(destination.PictureUrl), null);

            Assert.Empty(result);
        }
        [Theory]
        [InlineData("images/Items/Eclipse.jpg")]
        [InlineData("images/Items/BlackCleaver.jpg")]
        [InlineData("images/Items/FrozenMallet.jpg")]
        [InlineData("images/Items/LichBane.jpg")]
        [InlineData("images/Items/Statik.jpg")]
        public void Resolve_ReturnsFullUrl_WhenPictureUrlIsNotEmpty(string url)
        {
            var expectedApiUrl = "https://localhost:7224/";
            var cfgMock = new Mock<IConfiguration>();
            cfgMock.Setup(c => c["ApiUrl"]).Returns(expectedApiUrl);
            var resolver = new ItemUrlResolver(cfgMock.Object);
            var source = new Item { PictureUrl = url };
            var destination = new ItemDto();

            var result = resolver.Resolve(source, destination, nameof(destination.PictureUrl), null);

            var expectedUrl = expectedApiUrl + url;
            Assert.Equal(expectedUrl, result);
        }
    }
}
