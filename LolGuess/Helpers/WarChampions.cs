using API.DTO;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public static class WarChampions
    {
        public static List<Character> Generate(IReadOnlyList<Character> characters)
        {
            var rnd = new Random();
            var championsCount = characters.Count;
            var firstChampion = new Character();
            var secondChampion = new Character();

            do
            {
                firstChampion = characters[rnd.Next(championsCount)];
                secondChampion = characters[rnd.Next(championsCount)];
            }
            while (firstChampion == secondChampion);

            return new List<Character>() { firstChampion, secondChampion };

        }
        public static Func<CharacterDto, object>? GetSelector(PropertyEnum propertyEnum)
        {
            return propertyEnum switch
            {
                PropertyEnum.Hp => x => new { x.Name, x.PictureUrl, x.Hp },
                PropertyEnum.HpGain => x => new { x.Name, x.PictureUrl, x.HpGain },
                PropertyEnum.Mana => x => new { x.Name, x.PictureUrl, x.Mana },
                PropertyEnum.ManaGain => x => new { x.Name, x.PictureUrl, x.ManaGain },
                PropertyEnum.Ad => x => new { x.Name, x.PictureUrl, x.Ad },
                PropertyEnum.As => x => new { x.Name, x.PictureUrl, x.As },
                PropertyEnum.Armor => x => new { x.Name, x.PictureUrl, x.Armor },
                PropertyEnum.ArmorGain => x => new { x.Name, x.PictureUrl, x.ArmorGain },
                PropertyEnum.Mr => x => new { x.Name, x.PictureUrl, x.Mr },
                PropertyEnum.MS => x => new { x.Name, x.PictureUrl, x.MS },
                PropertyEnum.Range => x => new { x.Name, x.PictureUrl, x.Range },
                _ => null
            };
        }
    }
}
