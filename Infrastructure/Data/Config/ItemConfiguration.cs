using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.PictureUrl).IsRequired();
            builder.Property(c => c.Hp).HasConversion<double>();
            builder.Property(c => c.Mana).HasConversion<double>();
            builder.Property(c => c.Ad).HasConversion<double>();
            builder.Property(c => c.As).HasConversion<double>();
            builder.Property(c => c.Armor).HasConversion<double>();
            builder.Property(c => c.Mr).HasConversion<double>();
            builder.Property(c => c.MS).HasConversion<double>();
        }
    }
}
