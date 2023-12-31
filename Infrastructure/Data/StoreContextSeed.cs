﻿using Core.Entities;
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
                var championsData = File.ReadAllText("../Infrastructure/Data/SeedData/championsData.json");
                var champions = JsonSerializer.Deserialize<List<Character>>(championsData);

                ctx.Champions.AddRange(champions);
            }

            if (!ctx.Items.Any())
            {
                var itemsData = File.ReadAllText("../Infrastructure/Data/SeedData/itemData.json");
                var items = JsonSerializer.Deserialize<List<Item>>(itemsData);

                ctx.Items.AddRange(items);
            }

            if (ctx.ChangeTracker.HasChanges())
                await ctx.SaveChangesAsync();
        }
    }
}
