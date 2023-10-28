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
    }
}
