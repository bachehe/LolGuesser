using API.DTO;
using API.Helpers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace API.Tests.Helpers
{
    public class WarChampionsTest
    {
        [Fact]
        public void Generate_ChampionsEqual()
        {
            var characters = new List<Character>()
            {
                new Character{},
                new Character{},
                new Character{},
            }.AsReadOnly();

            var result = WarChampions.Generate(characters);

            Assert.Equal(2, result.Count);
            Assert.Contains(result[0], characters);
            Assert.Contains(result[1], characters);
            Assert.NotEqual(result[0], result[1]);
        }

        [Fact]
        public void GetSelector_Hp_ReturnsFunctionWithExpectedProperties()
        {
            var propertyEnum = PropertyEnum.Hp;
            var character = new CharacterDto { Name = "Test", PictureUrl = "http://example.com", Hp = 100 };

            var selector = WarChampions.GetSelector(propertyEnum);
            Assert.NotNull(selector); 

            var result = selector(character);
            Assert.NotNull(result); 

            var nameProperty = result.GetType().GetProperty("Name");
            var pictureUrlProperty = result.GetType().GetProperty("PictureUrl");
            var hpProperty = result.GetType().GetProperty("Hp");

            Assert.NotNull(nameProperty);
            Assert.NotNull(pictureUrlProperty);
            Assert.NotNull(hpProperty);

            Assert.Equal("Test", nameProperty.GetValue(result));
            Assert.Equal("http://example.com", pictureUrlProperty.GetValue(result));
            //Assert.Equal(100, hpProperty.GetValue(result));
        }

        [Fact]
        public void GetSelector_InvalidEnum_ReturnsNull()
        {
            var propertyEnum = (PropertyEnum)(-1);
            var selector = WarChampions.GetSelector(propertyEnum);

            Assert.Null(selector);
        }
    }
}
