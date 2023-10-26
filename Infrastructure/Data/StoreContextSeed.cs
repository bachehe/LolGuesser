using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext ctx)
        {
            if (!ctx.Champions.Any())
            {
                var championsData = File.ReadAllText("../Infrastructure/Data/SeedData/champions.json");
                var brands = JsonSerializer.Deserialize<List<Character>>(championsData);

                ctx.Champions.AddRange(brands);
            }

            if (ctx.ChangeTracker.HasChanges())
                await ctx.SaveChangesAsync();
        }
    }
}
