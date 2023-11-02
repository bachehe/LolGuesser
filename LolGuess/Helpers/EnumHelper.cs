namespace API.Helpers
{
    //public static class EnumHelper
    //{
    //    public static int GetEnumValues()
    //    {
    //        var random = new Random();
    //        var propertyValues = Enum.GetValues(typeof(PropertyEnum)).Cast<int>();
    //        return random.Next(propertyValues.Min(), propertyValues.Max() +1);
    //    }
    //}
    public static class EnumHelper
    {
        private static readonly Random Random = new Random();

        public static int GetRandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            return Random.Next(values.Min(), values.Max() + 1);
        }
    }
}
