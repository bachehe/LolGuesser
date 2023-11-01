namespace API.Helpers
{
    public enum PropertyEnum
    {
        Hp = 1,
        HpGain = 2,
        Mana = 3,
        ManaGain = 4,
        Ad = 5,
        As = 6,
        Armor = 7,
        ArmorGain = 8,
        Mr = 9,
        MS = 10,
        Range = 11
    }
    public static class EnumHelper
    {
        public static int GetEnumValues()
        {
            var random = new Random();
            var propertyValues = Enum.GetValues(typeof(PropertyEnum)).Cast<int>();
            return random.Next(propertyValues.Min(), propertyValues.Max() +1);
        }
    }

}
