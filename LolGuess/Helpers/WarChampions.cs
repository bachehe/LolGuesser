using API.DTO;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public static class WarChampions
    {
        private static Func<CharacterDto, object>? _selector;

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

        public static IEnumerable<object> SelectObjects(CharacterDto ch1, CharacterDto ch2, bool isShort)
        {
            var war = new List<CharacterDto>() { ch1, ch2 };
            int randomIndex;

            if (isShort)
            {
                randomIndex = EnumHelper.GetRandomEnumValue<ShortPropertyEnum>();
                _selector = WarChampions.GetSelector((ShortPropertyEnum)randomIndex);
            }
            else
            {
                randomIndex = EnumHelper.GetRandomEnumValue<PropertyEnum>();
                _selector = WarChampions.GetSelector((PropertyEnum)randomIndex);
            }


            return war.Select(_selector);
        }

        public static Func<CharacterDto, object>? GetSelector(PropertyEnum propertyEnum)
            => propertyEnum switch
            {
                PropertyEnum.HpGain => x => new { x.Name, x.PictureUrl, x.HpGain },
                PropertyEnum.ManaGain => x => new { x.Name, x.PictureUrl, x.ManaGain },
                PropertyEnum.ArmorGain => x => new { x.Name, x.PictureUrl, x.ArmorGain },
                PropertyEnum.Armor => x => new { x.Name, x.PictureUrl, x.Armor },
                PropertyEnum.Hp => x => new { x.Name, x.PictureUrl, x.Hp },
                PropertyEnum.Mana => x => new { x.Name, x.PictureUrl, x.Mana },
                PropertyEnum.Ad => x => new { x.Name, x.PictureUrl, x.Ad },
                PropertyEnum.As => x => new { x.Name, x.PictureUrl, x.As },
                PropertyEnum.Mr => x => new { x.Name, x.PictureUrl, x.Mr },
                PropertyEnum.MS => x => new { x.Name, x.PictureUrl, x.MS },
                PropertyEnum.Range => x => new { x.Name, x.PictureUrl, x.Range },
                _ => null
            };

        public static Func<CharacterDto, object>? GetSelector(ShortPropertyEnum propertyEnum)
           => propertyEnum switch
           {
               ShortPropertyEnum.Armor => x => new { x.Name, x.PictureUrl, x.Armor },
               ShortPropertyEnum.Hp => x => new { x.Name, x.PictureUrl, x.Hp },
               ShortPropertyEnum.Mana => x => new { x.Name, x.PictureUrl, x.Mana },
               ShortPropertyEnum.Ad => x => new { x.Name, x.PictureUrl, x.Ad },
               ShortPropertyEnum.As => x => new { x.Name, x.PictureUrl, x.As },
               ShortPropertyEnum.Mr => x => new { x.Name, x.PictureUrl, x.Mr },
               ShortPropertyEnum.MS => x => new { x.Name, x.PictureUrl, x.MS },
               _ => null
           };
    }
}
