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
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
             builder.Property( c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Hp).HasConversion<double>();
            builder.Property(c => c.Ap).HasConversion<double>();
            builder.Property(c => c.Ad).HasConversion<double>();
            builder.Property(c => c.HpGain).HasConversion<double>();
        }
    }
}
