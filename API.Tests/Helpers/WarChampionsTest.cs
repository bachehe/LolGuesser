using API.DTO;
using API.Helpers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        [Theory]
        [InlineData(PropertyEnum.Hp, 100, 100)]
        [InlineData(PropertyEnum.Ad, 55, 55)]
        [InlineData(PropertyEnum.Mana, 72, 72)]
        [InlineData(PropertyEnum.ManaGain, 311, 311)]
        [InlineData(PropertyEnum.HpGain, 1030, 1030)]
        [InlineData(PropertyEnum.As, 66, 66)]
        [InlineData(PropertyEnum.ArmorGain, 26, 26)]
        [InlineData(PropertyEnum.Armor, 41, 41)]
        [InlineData(PropertyEnum.Mr, 30, 30)]
        [InlineData(PropertyEnum.MS, 25, 25)]
        [InlineData(PropertyEnum.Range, 50, 50)]
        public void GetSelector_Hp_ReturnsFunctionWithExpectedProperties(PropertyEnum propEnum, decimal propertyValue, decimal expected)
        {
            var character = new CharacterDto
            {
                Name = "Test",
                PictureUrl = "http://example.com",
                HpGain = propertyValue,
                Hp = propertyValue,
                ManaGain = propertyValue,
                Ad = propertyValue,
                As = propertyValue,
                Armor = propertyValue,
                ArmorGain = propertyValue,
                Mr = propertyValue,
                MS = propertyValue,
                Range = propertyValue,
                Mana = propertyValue
            };

            var selector = WarChampions.GetSelector(propEnum);
            Assert.NotNull(selector);

            var result = selector(character);
            Assert.NotNull(result);

            var resultProperty = result.GetType().GetProperty(propEnum.ToString());
            Assert.NotNull(resultProperty);

            Assert.Equal(expected, resultProperty.GetValue(result));
        }

        [Fact]
        public void GetSelector_InvalidEnum_ReturnsNull()
        {
            var propertyEnum = (PropertyEnum)(-1);
            var selector = WarChampions.GetSelector(propertyEnum);

            Assert.Null(selector);
        }

        //TODO
        [Fact]
        public void SelectObjects_EmptyList_ReturnsNotNull()
        {
            var ch1 = new CharacterDto { };
            var ch2 = new CharacterDto { };

            var result = WarChampions.SelectObjects(ch1, ch2).ToList();

            Assert.NotNull(result);
        }
    }
   
}
