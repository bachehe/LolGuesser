namespace API.Helpers
{
    public static class EnumHelper
    {
        private static readonly Random _random = new Random();

        public static int GetRandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            return _random.Next(values.Min(), values.Max() + 1);
        }
    }
}
