using API.DTO;
using Core.Entities;

namespace API.Helpers
{
    public static class WarChampions
    {
        #region const
        private const string NameAttribute = "Name";
        private const string MSAttribute = "MS";
        private const string AsAttribute = "As";
        private const string PicAttribute = "PictureUrl";
        private const string ManaAttribute = "Mana";
        private const int RequestedItemsCount = 4;
        private const int ManaChecker = 10;
        #endregion

        private static Func<CharacterDto, object>? _warProperties;

        public static List<Character> GenerateWarChampions(IReadOnlyList<Character> characters)
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

        public static List<Item> GenerateItems(IReadOnlyList<Item> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var rnd = new Random();
            var result = new List<Item>();

            for (int n = 0; n < RequestedItemsCount; n++)    
                result.Add(items[rnd.Next(items.Count)]);
           

            return result;
        }

        public static IEnumerable<object> SelectObjects(List<CharacterDto> characters, bool isShort)
        {
            int randomIndex;

            if (characters.Count == 0)
                return new List<object>();

            if (isShort)
            {
                randomIndex = EnumHelper.GetRandomEnumValue<ShortPropertyEnum>();
                _warProperties = WarChampions.GetSelector((ShortPropertyEnum)randomIndex);
            }
            else
            {
                randomIndex = EnumHelper.GetRandomEnumValue<PropertyEnum>();
                _warProperties = WarChampions.GetSelector((PropertyEnum)randomIndex);
            }

            return characters.Select(_warProperties);
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

        public static void MergeChampionWithItems(CharacterDto champion, List<ItemDto> items)
        {
            foreach (var item in items)
            {
                foreach (var property in typeof(CharacterDto).GetProperties())
                {
                    var itemProperty = typeof(ItemDto).GetProperty(property.Name);

                    if (itemProperty == null) continue;

                    var championValue = property.GetValue(champion);
                    var itemValue = itemProperty.GetValue(item);

                    if (property.Name == NameAttribute || property.Name == PicAttribute) continue;

                    if (property.Name == ManaAttribute && (decimal)championValue < ManaChecker) continue;

                    if (property.Name == MSAttribute || property.Name == AsAttribute)
                    {
                        property.SetValue(champion, (decimal)championValue * (1 + (decimal)itemValue));
                        continue;
                    }

                    property.SetValue(champion, (decimal)championValue + (decimal)itemValue);
                }
            }
        }
        public static ChampionItemDto ChampionsAndItemsList(IEnumerable<object> champions, List<ItemDto> items)
        {
            var itemNames = new List<string>();
            var itemPictureUrls = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                itemNames.Add(items[i].Name);
                itemPictureUrls.Add(items[i].PictureUrl);
            }

            return new ChampionItemDto
            {
                Character = champions,
                Item = itemNames,
                ItemPictureUrl = itemPictureUrls,
            };
        }
    }
}
