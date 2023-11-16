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

            var result = WarChampions.GenerateWarChampions(characters);

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

        [Fact]
        public void CreateChampionsWithItemList_EmptyLists_ReturnExpected()
        {
            var champions = new List<object>();
            var items = new List<ItemDto>();

            var result = WarChampions.CreateChampionsWithItemList(champions, items);

            Assert.Empty(result.Character);
            Assert.Empty(result.Item);
            Assert.Empty(result.ItemPictureUrl);
        }

        [Fact]
        public void CreateChampionsWithItemList_ReturnsExpected()
        {
            var mockChampions = new List<object> { };
            var mockItems = new List<ItemDto>
            {
                new ItemDto { Name = "Item1", PictureUrl = "url" },
                new ItemDto { Name = "Item2", PictureUrl = "url" }
            };

            var result = WarChampions.CreateChampionsWithItemList(mockChampions, mockItems);

            Assert.Equal(mockChampions, result.Character);
            Assert.Equal(2, result.Item.Count());
            Assert.Contains("Item1", result.Item);
            Assert.Contains("url", result.ItemPictureUrl);
            Assert.Contains("Item2", result.Item);
            Assert.Contains("url", result.ItemPictureUrl);
        }

        [Theory]
        [InlineData(50, 50, 0.1, 50, 5, 0.1, 50, 50, 0.1, 50, 50, 0.1, 50, 50, 0.01, 50, 50, 0.1, 150, 150, 0.1111, 150, 5, 0.121)]
        [InlineData(50, 50, 1, 50, 100, 1, 50, 50, 1, 50, 50, 1,50, 50, 1, 50, 50, 1,150, 150, 0.4, 150, 200, 4)]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0,0, 0, 0.1, 0, 0, 0)]
        public void MergeChampionWithItems_ForProperties_CheckIfItsEqual(
            int ad, int armor, decimal As, int hp, int mana, decimal ms,
            int iad1, int iarmor1, decimal iAs1, int ihp1, int imana1, decimal ims1,
            int iad2, int iarmor2, decimal iAs2, int ihp2, int imana2, decimal ims2,
            int ead, int earmor, decimal eAs, int ehp, int emana, decimal ems)
        {
            var champion = new CharacterDto()
            {
                PictureUrl = "url",
                Name = "name",
                Ad = ad,
                Armor = armor,
                As = 0.1m,
                ArmorGain = As,
                Hp = hp,
                HpGain = 50,
                Mana = mana,
                ManaGain = 50,
                Mr = 50,
                MS = ms,
                Range = 50
            };
            var items = new List<ItemDto>()
            {
                new ItemDto()
                {
                    Name = "name",
                    Ad = iad1,
                    MS = ims1,
                    Mr= 50,
                    Armor = iarmor1,
                    As = iAs1,
                    Hp = ihp1,
                    Mana= imana1,
                    PictureUrl= "url",
                },
                new ItemDto()
                {
                    Name = "name",
                    Ad = iad2,
                    MS = ims2,
                    Mr= 50,
                    Armor = iarmor2,
                    As = iAs2,
                    Hp = ihp2,
                    Mana= imana2,
                    PictureUrl= "url",
                }
            };

            WarChampions.MergeChampionWithItems(champion, items);

            Assert.Equal(ead, champion.Ad);
            Assert.Equal(earmor, champion.Armor);
            Assert.Equal(eAs, champion.As);
            Assert.Equal(ehp, champion.Hp);
            Assert.Equal(emana, champion.Mana);
            Assert.Equal(ems, champion.MS);
        }

        //TODO
        [Fact]
        public void SelectObjects_EmptyList_ReturnsNotNull()
        {
            var list = new List<CharacterDto>();

            var result = WarChampions.SelectObjects(list, false).ToList();

            Assert.NotNull(result);
        }
    }

}
